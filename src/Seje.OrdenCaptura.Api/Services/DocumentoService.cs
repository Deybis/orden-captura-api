using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Seje.FileManager.Client;
using Seje.FileManager.Client.Models;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.Api.Models;
using Seje.OrdenCaptura.Api.Utils;
using Seje.OrdenCaptura.Api.Validator;
using Seje.OrdenCaptura.QueryStack;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Services
{
    public class DocumentoService : IDocumento
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentoService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Documento> Repository { get; }
        private readonly IFileManagerClient _fileManagerClient;
        public IConfiguration _configuration { get; }
        public const string SystemName = "FileManagerSettings:SystemName";
        public const string LocalFilePath = "LocalFilePath";

        public DocumentoService(
        IMapper mapper,
        ILogger<DocumentoService> logger,
        IMediator mediator,
        IRepository<QueryStack.Documento> repository,
        IFileManagerClient fileManagerClient,
        IConfiguration configuration
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
            _fileManagerClient = fileManagerClient;
            _configuration = configuration;
        }
        public async Task<Result<List<Documento>>> List()
        {
            var result = new Result<List<Documento>>(true, null, new List<Documento>());
            var documentos = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Documento>>(documentos);
            return result;
        }

        public async Task<Result<Documento>> GetById(long documentoId)
        {
            var result = new Result<Documento>(true, null, new Documento());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var documento = await Repository.GetByIdAsync(documentoId,ct.Token);

            if (documento == null)
            {
                result.Message = "No se encontró información del documento";
                return result;
            }

            result.Entity = _mapper.Map<Documento>(documento);

            return result;
        }

        public async Task<Result<List<Documento>>> GetByFilter(FiltrosDocumento filtros)
        {
            var result = new Result<List<Documento>>(false, null, new List<Documento>());
            try
            {
                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var documentos = await Repository.ListAsync(new DocumentoSpec(filtros), ct.Token);
                result.Message = !documentos.Any() ? "No se encontró documentos con la información proporcionada" : string.Empty;
                result.Entity = documentos.Any() ? _mapper.Map<List<Documento>>(documentos) : result.Entity;

                foreach (var item in result?.Entity)
                {
                    var file = await _fileManagerClient.GetUrl(item.Codigo);
                    item.Base64String = file.fileBase64String;
                    item.Url = file.Url;
                }
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Message = "Ocurrio un error al intentar recuperar uno o más documentos";
                return result;
            }
        }

        public async Task<Result<Documento>> Create(Documento model, string userName)
        {
            var result = new Result<Documento>(false, null, new Documento());

            try
            {
                var validator = new RegistrarDocumentoValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Documento>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var documento = await Repository.GetBySpecAsync(new DocumentoSpec(new FiltrosDocumento
                {
                    NumeroOrdenCaptura = model.NumeroOrdenCaptura,
                    Nombre = model.Nombre
                }), ct.Token);

                if (documento != null)
                {
                    result.Message = "Ya existe un documento con la información proporcionada";
                    return result;
                }

                var create = await Repository.AddAsync(_mapper.Map<QueryStack.Documento>(model),ct.Token);
                model.DocumentoId = create.DocumentoId;
                result.Entity = model;
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

        public async Task<Result<List<RegistrarDocumento>>> CreateMultiple(List<RegistrarDocumento> documentos, string userName)
        {
            var result = new Result<List<RegistrarDocumento>>(false, null, new List<RegistrarDocumento>());
            string numeroOrdenCaptura = string.Empty;
            string descripcion = string.Empty;
            try
            {
                foreach (var documento in documentos)
                {
                    numeroOrdenCaptura = documento.NumeroOrdenCaptura;
                    descripcion = documento.Nombre;
                    documento.Nombre = $"{documento.Nombre.Replace(documento.Extension, "")}-{documento.NumeroOrdenCaptura}{documento.Extension}";
                    documento.Base64String = Util.CleanBase64String(documento.Base64String);

                    var documentoExiste = await Repository.GetBySpecAsync(new DocumentoSpec(new FiltrosDocumento {
                        NumeroOrdenCaptura = documento.NumeroOrdenCaptura,
                        Nombre = documento.Nombre
                    }));
                    if (documentoExiste != null) break;

                    EscribirEnDirectorioLocal(documento);

                    #region Guardar en FileManager
                    var archivo = new Archivo
                    {
                        Id = Guid.NewGuid(),
                        Description = string.Empty,
                        FilePath = documento.FilePath,
                        UserName = userName ?? "anonimo",
                        FileName = documento.Nombre,
                        SourceSystem = _configuration.GetValue<string>(SystemName),
                        DocumentExtension = documento.Extension
                    };
                    bool response = await _fileManagerClient.UploadFile(archivo);
                    #endregion

                    #region Guardar referencia en entidad documentos del componente
                    if (response)
                    {
                        Documento doc = new Documento
                        {
                            Codigo = archivo.Id,
                            Descripcion = descripcion,
                            Extension = documento.Extension,
                            Nombre = documento.Nombre,
                            NumeroOrdenCaptura = documento.NumeroOrdenCaptura,
                            TipoMedia = documento.Tipo,
                            TipoDocumentoId = documento.TipoDocumentoId,
                            Ubicacion = documento.FilePath,
                            Peso = documento.Size,
                            OrdenCapturaId = documento.OrdenCapturaId,
                            Finalizado = true,
                            UsuarioCreacion = userName,
                            UsuarioModificacion = userName,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        var create = await Repository.AddAsync(_mapper.Map<QueryStack.Documento>(doc));
                    }
                    #endregion
                    result.Success = response;
                }
                EliminarDirectorioLocal(numeroOrdenCaptura);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                result.Message = ex.Message;
                return result;
            }
        }

        public async Task<Result<Documento>> Update(Documento model, string userName)
        {
            var result = new Result<Documento>(true, null, new Documento());
            try
            {
                var validator = new ActualizarDocumentoValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<Documento>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.Documento>(model));
                result.Entity = _mapper.Map<Documento>(model);
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

        public async Task<Result<Documento>> Delete(long id, string userName)
        {
            var result = new Result<Documento>(true, null, new Documento());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Message = "Documento eliminado exitosamente";
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

        private void EscribirEnDirectorioLocal(RegistrarDocumento doc)
        {
            if (!string.IsNullOrWhiteSpace(doc.Base64String))
            {
                string directory = Path.Combine(_configuration.GetValue<string>(LocalFilePath), $"{doc.NumeroOrdenCaptura}");
                doc.FilePath = Path.Combine(directory, doc.Nombre);
                Directory.CreateDirectory(directory);
                File.WriteAllBytes(doc.FilePath, Convert.FromBase64String(doc.Base64String));
            }
        }

        private void EliminarDirectorioLocal(string numeroOrdenCaptura)
        {
            string directory = Path.Combine(_configuration.GetValue<string>(LocalFilePath), $"{numeroOrdenCaptura}");
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
        }
    }
}
