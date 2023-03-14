using AutoMapper;
using Entities.Shared.Model;
using Entities.Shared.Paging.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QRCoder;
using Seje.Authorization.Service;
using Seje.FileManager.Client;
using Seje.Firma.Client;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.Api.Models;
using Seje.OrdenCaptura.Api.Utils;
using Seje.OrdenCaptura.Api.Validator;
using Seje.OrdenCaptura.QueryStack;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace Seje.OrdenCaptura.Api.Services
{
    public class OrdenCapturaService : IOrdenCaptura
    {
        private readonly IMapper _mapper;
        private readonly ITipoFirma _tipoFirmaService;
        private readonly IDocumento _documentoService;
        private readonly ILogger<OrdenCapturaService> _logger;
        private readonly IFirmaDigitalClient _firmaDigitalClient;
        private readonly IFileManagerClient _fileManagerClient;
        private readonly IAuthorizationService _authorizationService;
        public readonly IConverter _converter;
        public int OrganoJurisdiccional { get; set; }
        public const string SystemName = "FileManagerSettings:SystemName";
        public const string Component = "AuthorizationSettings:Component";
        public const string LocalFilePath = "LocalFilePath";
        public const string LogoFilePath = "LogoFilePath";
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Firma> FirmaRepository { get; }
        public IRepository<QueryStack.OrdenCaptura> Repository { get; }
        public IConfiguration _configuration { get; }


        public OrdenCapturaService(
        IMapper mapper,
        ILogger<OrdenCapturaService> logger,
        IMediator mediator,
        IRepository<QueryStack.OrdenCaptura> repository,
        IRepository<QueryStack.Firma> firmaRepository,
        IHttpContextAccessor httpContextAccessor,
        IFirmaDigitalClient firmaDigitalClient,
        IFileManagerClient fileManagerClient,
        IAuthorizationService authorizationService,
        IConverter converter,
        IDocumento documentoService,
        ITipoFirma tipoFirmaService,
        IConfiguration configuration
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
            FirmaRepository = firmaRepository;
            int.TryParse(httpContextAccessor.HttpContext.Request.Headers["organo_jurisdiccional_id"].FirstOrDefault(), out int value);
            OrganoJurisdiccional = value;
            _firmaDigitalClient = firmaDigitalClient;
            _documentoService = documentoService;
            _fileManagerClient = fileManagerClient;
            _authorizationService = authorizationService;
            _tipoFirmaService = tipoFirmaService;
            _configuration = configuration;
            _converter = converter;
        }
        public async Task<Result<List<OrdenCaptura>>> List()
        {
            var result = new Result<List<OrdenCaptura>>(true, null, new List<OrdenCaptura>());
            var ordenesCaptura = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<OrdenCaptura>>(ordenesCaptura.OrderByDescending(x => x.OrdenCapturaId));
            return result;
        }

        public async Task<PagedResult<OrdenCaptura>> List(FiltrosOrdenCaptura filtros, string userName)
        {
            PagedResult<OrdenCaptura> response = new();

            if (OrganoJurisdiccional == 0) return response;

            filtros.OrganoJurisdiccionalId = OrganoJurisdiccional;

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var ordenes = await Repository.ListAsync(new OrdenCapturaSpec(filtros), ct.Token);
            ordenes = await FiltrarOrdenCapturaPermiso(ordenes, userName);
            response.TotalCount = ordenes.Count();

            var skip = (filtros.PageNumber - 1) * filtros.PageSize;
            var take = filtros.PageSize;

            response.Result = _mapper.Map<List<OrdenCaptura>>(ordenes.Skip(skip).Take(take));
            response.PageNumber = filtros.PageNumber;
            response.PageSize = filtros.PageSize;
            return response;
        }

        public async Task<Result<OrdenCaptura>> GetById(long ordenCapturaId)
        {
            var result = new Result<OrdenCaptura>(true, null, new OrdenCaptura());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var ordenCaptura = await Repository.GetByIdAsync(ordenCapturaId, ct.Token);

            if (ordenCaptura == null)
            {
                result.Message = "No se encontró información de la orden de captura";
                return result;
            }

            result.Entity = _mapper.Map<OrdenCaptura>(ordenCaptura);

            return result;
        }

        public async Task<Result<List<OrdenCaptura>>> GetByFilter(FiltrosOrdenCaptura filtros)
        {
            var result = new Result<List<OrdenCaptura>>(true, null, new List<OrdenCaptura>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var ordenesCaptura = await Repository.ListAsync(new OrdenCapturaSpec(filtros), ct.Token);
            result.Message = !ordenesCaptura.Any() ? "No se encontró ordenes de captura con la información proporcionada" : string.Empty;

            result.Entity = ordenesCaptura.Any() ? _mapper.Map<List<OrdenCaptura>>(ordenesCaptura.OrderByDescending(x => x.OrdenCapturaId)) : result.Entity;
            return result;
        }

        public async Task<Result<OrdenCaptura>> Create(RegistrarOrdenCaptura model, string userName)
        {
            var result = new Result<OrdenCaptura>(false, null, new OrdenCaptura());
            try
            {
                var validator = new RegistrarOrdenCapturaValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<OrdenCaptura>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var ordenesCaptura = await Repository.ListAsync(new OrdenCapturaSpec(new FiltrosOrdenCaptura
                {
                    NumeroExpediente = model.NumeroExpediente,
                }), ct.Token);

                var ordenCapturaVigente = ordenesCaptura.Where(x => x.OrdenCapturaEstadoId != (int)OrdenCapturaEstados.Finalizada).ToList();

                if (ordenCapturaVigente.Any())
                {
                    result.Message = $"Existe una orden de captura vigente para el expediente: {model.NumeroExpediente}";
                    result.Success = false;
                    return result;
                }

                var ordenCapturaEstadoId = (int)OrdenCapturaEstados.Borrador;
                var ordenCapturaEstadoDescripcion = Enum.GetName(typeof(OrdenCapturaEstados), ordenCapturaEstadoId);
                var ordenCapturaCodigo = Guid.NewGuid();
                var command = new CommandStack.Commands.RegistrarOrdenCapturaCommand(0,
                    model.OrganoJurisdiccionalId,
                    ordenCapturaCodigo,
                    model.NumeroOrdenCaptura,
                    model.Correlativo,
                    ordenCapturaEstadoId,
                    ordenCapturaEstadoDescripcion,
                    model.ExpedienteId,
                    model.NumeroExpediente,
                    model.InstanciaId,
                    model.InstanciaDescripcion,
                    model.CorreoSecretario,
                    model.CorreoJuez,
                    model.CorreoEscribiente,
                    Convert.ToDateTime(model.FechaEmision),
                    model.AlertaInternacional,
                    model.DepartamentoId,
                    model.DepartamentoDescripcion,
                    model.MunicipioId,
                    model.MunicipioDescripcion,
                    userName);
                var create = await Mediator.Send(command);
                result.Success = true;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<Result<OrdenCaptura>> Update(ActualizarOrdenCaptura model, string userName)
        {
            var result = new Result<OrdenCaptura>(true, null, new OrdenCaptura());
            try
            {
                var validator = new ActualizarOrdenCapturaValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<OrdenCaptura>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var command = new CommandStack.Commands.ModificarOrdenCapturaCommand(model.OrdenCapturaId,
                    model.OrganoJurisdiccionalId,
                    model.OrdenCapturaCodigo,
                    model.NumeroOrdenCaptura,
                    model.Correlativo,
                    model.OrdenCapturaEstadoId,
                    model.OrdenCapturaEstadoDescripcion,
                    model.ExpedienteId,
                    model.NumeroExpediente,
                    model.InstanciaId,
                    model.InstanciaDescripcion,
                    model.CorreoSecretario,
                    model.CorreoJuez,
                    model.CorreoEscribiente,
                    Convert.ToDateTime(model.FechaEmision),
                    Convert.ToDateTime(model.FechaEntrega),
                    model.AlertaInternacional,
                    model.DepartamentoId,
                    model.DepartamentoDescripcion,
                    model.MunicipioId,
                    model.MunicipioDescripcion,
                    model.Observaciones,
                    userName);
                await Mediator.Send(command);

                result.Entity = _mapper.Map<OrdenCaptura>(model);
                return result;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<Result<List<Firma>>> AgregarFirmas(string numeroOrdenCaptura, int numeroFirmas, string userName)
        {
            var result = new Result<List<Firma>>(false, null, new List<Firma>());
            try
            {
                var orden = await Repository.GetBySpecAsync(new OrdenCapturaSpec(new FiltrosOrdenCaptura { NumeroOrdenCaptura = numeroOrdenCaptura }));
                if (orden == null)
                {
                    result.Success = false;
                    return result;
                }

                var tipoFirmas = await _tipoFirmaService.List();

                foreach (var item in tipoFirmas?.Entity)
                {
                    for (int i = 0; i < numeroFirmas; i++)
                    {
                        QueryStack.Firma firma = new QueryStack.Firma();
                        firma.CorreoFirmante = i == 0 ? orden.CorreoSecretario : orden.CorreoJuez;
                        firma.Firmo = false;
                        firma.NumeroOrdenCaptura = orden.NumeroOrdenCaptura;
                        firma.OrdenCapturaId = orden.OrdenCapturaId;
                        firma.TipoFirmaId = item.TipoFirmaId;
                        firma.UsuarioCreacion = userName;
                        firma.FechaCreacion = DateTime.Now;
                        firma.UsuarioModificacion = userName;
                        firma.FechaModificacion = DateTime.Now;
                        await FirmaRepository.AddAsync(firma);
                    }
                }
                result.Success = true;

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<Result<FirmaResponse>> Firmar(FirmaRequest model, string userName)
        {
            _logger.LogInformation("Entró a firmar");
            var result = new Result<FirmaResponse>(false, null, new FirmaResponse());
            List<OrdenCapturaPDF> documentos = new List<OrdenCapturaPDF>();
            int firmasRealizadas = 0;
            try
            {
                #region Generar/Obtener Documentos
                var firmasOrdenCaptura = await FirmaRepository.ListAsync(new FirmaSpec(new FiltrosFirma
                {
                    TipoFirmaId = model.TipoFirma,
                    NumeroOrdenCaptura = model.OrdenCapturaFormato.NumeroOrdenCaptura
                }));
                firmasRealizadas = firmasOrdenCaptura?.Where(x => x.Firmo == true).Count() ?? 0;

                if (firmasRealizadas == 0) documentos = GenerarDocumentos(model.TipoDocumento, model.OrdenCapturaFormato);
                else documentos = await ObtenerDocumentos(documentos, model);
                #endregion

                #region FirmarDocumentos
                foreach (var doc in documentos)
                {
                    _logger.LogInformation($"Firmando documento: {doc.FileName}");
                    model.File = doc.PdfBase64;
                    result = await _firmaDigitalClient.Firmar(model);
                    doc.PdfBase64 = result.Entity.File; //--> Documento firmado
                    EscribirDocumentoLocal(doc, model.OrdenCapturaFormato.NumeroOrdenCaptura);
                }
                #endregion

                #region Guardar/Actualizar
                if (result.Success)
                {
                    firmasRealizadas++;
                    bool firmasFinalizadas = firmasOrdenCaptura?.Count == firmasRealizadas;
                    var documentResult = await GuardarDocumentos(model, documentos, userName, firmasFinalizadas);
                    var actualizarFirmaResult = await ActualizarFirma(firmasOrdenCaptura, model.UserName, documentResult);
                    await ActualizarEstadoOrdenCaptura(model.TipoFirma, firmasFinalizadas, model.OrdenCapturaFormato.NumeroOrdenCaptura, userName, actualizarFirmaResult);
                }
                #endregion

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Success = false;
                return result;
            }
        }

        public List<OrdenCapturaPDF> GenerarDocumentos(int tipoDocumento, OrdenCapturaFormato formato)
        {
            List<OrdenCapturaPDF> documentos = new List<OrdenCapturaPDF>();
            try
            {
                foreach (var institucion in formato.Instituciones)
                {
                    OrdenCapturaPDF documento = new();

                    string qrData = JsonConvert.SerializeObject(new Dictionary<string, string>(){
                        {"NumeroOrdenCaptura", formato.NumeroOrdenCaptura},
                        {"NombreImputado", formato.ExpedienteDetalle.NombreImputado},
                        {"Victimas", formato.ExpedienteDetalle.Victimas},
                        {"Delitos", formato.ExpedienteDetalle.Delitos},
                        {"FechaEmision", formato.FechaEmision},
                        {"EmitidaPor", formato.Juzgado}
                    }, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    string qr = GenerarQR(qrData);
                    string logo = ObtenerLogo();

                    string template = Util.GetTemplate(tipoDocumento == (int)TipoDocumento.OrdenCaptura ? "OrdenCaptura.html" : "ContraOrdenCaptura.html", formato, institucion.Nombre, qr, logo);

                    byte[] res = null;
                    var doc = new HtmlToPdfDocument()
                    {
                        GlobalSettings = {
                            ColorMode = ColorMode.Color,
                            Orientation = Orientation.Portrait,
                            PaperSize = PaperKind.Letter,
                        },
                        Objects = {
                            new ObjectSettings() {
                                PagesCount = true,
                                HtmlContent = template,
                                WebSettings = { DefaultEncoding = "utf-8" }
                            }
                        }
                    };
                    res = _converter.Convert(doc);

                    documento.PdfBase64 = Convert.ToBase64String(res);
                    documento.Institucion = institucion.Nombre;
                    documento.FileName = tipoDocumento == (int)TipoDocumento.OrdenCaptura
                        ? $"OrdenCaptura-{formato.NumeroOrdenCaptura}-{institucion.Nombre}.pdf"
                        : $"Contra-OrdenCaptura-{formato.NumeroOrdenCaptura}-{institucion.Nombre}.pdf";
                    documentos.Add(documento);
                }
                return documentos;

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error al generar documento:{ex.Message}");
                return documentos;
            }
        }

        private async Task<bool> GuardarDocumentos(FirmaRequest model, List<OrdenCapturaPDF> documentos, string userName, bool finalizado)
        {
            var result = false;
            try
            {
                foreach (var documento in documentos)
                {
                    #region Guardar en FileManager
                    var archivo = new FileManager.Client.Models.Archivo
                    {
                        Id = Guid.NewGuid(),
                        DirectoryId = Guid.NewGuid(),
                        Description = string.Empty,
                        FilePath = documento.FilePath,
                        UserName = userName ?? "anonimo",
                        FileName = documento.FileName,
                        SourceSystem = _configuration.GetValue<string>(SystemName),
                        DocumentExtension = ".pdf"
                    };
                    documento.FileName = archivo.FileName;
                    result = await _fileManagerClient.UploadFile(archivo);
                    #endregion

                    #region Guardar referencia en entidad documentos del componente
                    if (result)
                    {
                        FileInfo fileInfo = new FileInfo(documento.FilePath);
                        Documento doc = new Documento
                        {
                            Codigo = archivo.Id,
                            Descripcion = documento.FileName,
                            Extension = archivo.DocumentExtension,
                            Nombre = documento.FileName,
                            NumeroOrdenCaptura = model.OrdenCapturaFormato.NumeroOrdenCaptura,
                            TipoMedia = "application/pdf",
                            TipoDocumentoId = model.TipoDocumento,
                            Ubicacion = documento.FilePath,
                            Peso = fileInfo.Length,
                            OrdenCapturaId = model.OrdenCapturaFormato.OrdenCapturaId,
                            Finalizado = finalizado,
                            UsuarioCreacion = userName,
                            UsuarioModificacion = userName,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        var response = await _documentoService.Create(doc, userName);
                    }
                    #endregion
                }
                EliminarDirectorioLocal(model.OrdenCapturaFormato.NumeroOrdenCaptura);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ocurrio un error al guardar documento:{ex.Message}");
                result = false;
                return result;
            }
        }

        private async Task<bool> ActualizarFirma(List<QueryStack.Firma> firmasOrdenCaptura, string userName, bool documentResult)
        {
            if (!documentResult) return false;
            bool result = false;
            try
            {
                var firma = firmasOrdenCaptura?.Where(x => x.CorreoFirmante == userName).FirstOrDefault();
                if (firma != null)
                {
                    firma.Firmo = true;
                    firma.FechaModificacion = DateTime.Now;
                    await FirmaRepository.UpdateAsync(firma);
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ocurrio un error al actualizar la firma:{ex.Message}");
                return result;
            }
        }

        private async Task<bool> ActualizarEstadoOrdenCaptura(int tipoFirma, bool firmasFinalizadas, string numeroOrdenCaptura, string userName, bool actualizarFirmaResult)
        {
            if (!actualizarFirmaResult) return false;
            try
            {
                if (firmasFinalizadas)
                {
                    var model = await Repository.GetBySpecAsync(new OrdenCapturaSpec(new FiltrosOrdenCaptura { NumeroOrdenCaptura = numeroOrdenCaptura }));
                    model.OrdenCapturaEstadoId = tipoFirma == (int)TipoFirmas.OrdenCaptura ? (int)OrdenCapturaEstados.PendienteDeEntrega : (int)OrdenCapturaEstados.ContraCaptura;
                    model.OrdenCapturaEstadoDescripcion = Enum.GetName(typeof(OrdenCapturaEstados), model.OrdenCapturaEstadoId);
                    var command = new CommandStack.Commands.ModificarOrdenCapturaCommand(model.OrdenCapturaId,
                        model.OrganoJurisdiccionalId, model.OrdenCapturaCodigo,
                        model.NumeroOrdenCaptura, model.Correlativo,
                        model.OrdenCapturaEstadoId, model.OrdenCapturaEstadoDescripcion,
                        model.ExpedienteId, model.NumeroExpediente,
                        model.InstanciaId, model.InstanciaDescripcion,
                        model.CorreoSecretario, model.CorreoJuez,
                        model.CorreoEscribiente, Convert.ToDateTime(model.FechaEmision),
                        Convert.ToDateTime(model.FechaEntrega), model.AlertaInternacional,
                        model.DepartamentoId, model.DepartamentoDescripcion,
                        model.MunicipioId, model.MunicipioDescripcion,
                        model.Observaciones, userName);
                    await Mediator.Send(command);
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ocurrio un error al actualizar el estado de la orden de captura:{ex.Message}");
                return false;
            }
        }

        private async Task<List<OrdenCapturaPDF>> ObtenerDocumentos(List<OrdenCapturaPDF> documentos, FirmaRequest model)
        {
            var documentosEntidad = await _documentoService.GetByFilter(new FiltrosDocumento
            {
                TipoDocumentoId = model.TipoDocumento,
                NumeroOrdenCaptura = model.OrdenCapturaFormato.NumeroOrdenCaptura
            });
            foreach (var item in documentosEntidad?.Entity)
            {
                OrdenCapturaPDF doc = new();
                doc.FileName = $"{item.Nombre.Replace(".pdf", "-F.pdf")}";
                doc.FilePath = item.Ubicacion;
                var response = await _fileManagerClient.GetUrl(item.Codigo);
                doc.PdfBase64 = response?.fileBase64String;
                documentos.Add(doc);
            }
            return documentos;
        }

        private void EscribirDocumentoLocal(OrdenCapturaPDF doc, string numeroOrdenCaptura)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(doc.PdfBase64))
                {
                    string directory = Path.Combine(_configuration.GetValue<string>(LocalFilePath), $"{numeroOrdenCaptura}");
                    doc.FilePath = Path.Combine(directory, doc.FileName);
                    Directory.CreateDirectory(directory);
                    File.WriteAllBytes(doc.FilePath, Convert.FromBase64String(doc.PdfBase64));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ocurrio un error al escribir el documento en el directorio temporal:{ex.Message}");
            }
        }

        private void EliminarDirectorioLocal(string numeroOrdenCaptura)
        {
            try
            {
                string directory = Path.Combine(_configuration.GetValue<string>(LocalFilePath), $"{numeroOrdenCaptura}");
                if (Directory.Exists(directory))
                    Directory.Delete(directory, true);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ocurrio un error al eliminar el directorio temporal:{ex.Message}");
            }
        }

        private static string GenerarQR(string secureQRCode)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(secureQRCode, QRCodeGenerator.ECCLevel.Q);
            return new Base64QRCode(qrCodeData).GetGraphic(20);
        }

        private string ObtenerLogo()
        {
            string logo = string.Empty;
            try
            {
                string logoUrl = _configuration.GetValue<string>(LogoFilePath);
                using (Image image = Image.FromFile(logoUrl))
                {
                    using MemoryStream m = new();
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    logo = Convert.ToBase64String(imageBytes);
                }
                return logo;
            }
            catch (Exception)
            {
                _logger.LogInformation($"Ocurrio un error al intentar generar logo: {logo}");
                return logo;
            }
        }

        private async Task<List<QueryStack.OrdenCaptura>> FiltrarOrdenCapturaPermiso(List<QueryStack.OrdenCaptura> ordenes, string userName)
        {
            List<QueryStack.OrdenCaptura> ordenesFiltradas = new();

            var permisos = await _authorizationService.GetPermissionsBy(userName, _configuration.GetValue<string>(Component));

            foreach (var permiso in permisos)
            {
                var nombrePermiso = permiso.Name.Trim();
                ordenesFiltradas.AddRange(ordenes?.Where(x => x.OrdenCapturaEstadoDescripcion.Replace(" ", "") == nombrePermiso).Select(x => x).ToList());
            }
            ordenesFiltradas = ordenesFiltradas.OrderByDescending(x => x.OrdenCapturaId).ToList();
            return ordenesFiltradas;
        }
    }
}
