using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.Api.Models;
using Seje.OrdenCaptura.Api.Validator;
using Seje.OrdenCaptura.QueryStack;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Services
{
    public class TipoDocumentoService : ITipoDocumento
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TipoDocumentoService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.TipoDocumento> Repository { get; }

        public TipoDocumentoService(
        IMapper mapper,
        ILogger<TipoDocumentoService> logger,
        IMediator mediator,
        IRepository<QueryStack.TipoDocumento> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<Models.TipoDocumento>>> List()
        {
            var result = new Result<List<Models.TipoDocumento>>(true, null, new List<Models.TipoDocumento>());
            var tipoDocumentos = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Models.TipoDocumento>>(tipoDocumentos);
            return result;
        }

        public async Task<Result<Models.TipoDocumento>> GetById(int tipoDocumentoId)
        {
            var result = new Result<Models.TipoDocumento>(true, null, new Models.TipoDocumento());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var tipoDocumento = await Repository.GetByIdAsync(tipoDocumentoId, ct.Token);

            if (tipoDocumento == null)
            {
                result.Message = "No se encontró información del tipo de documento";
                return result;
            }
            result.Entity = _mapper.Map<Models.TipoDocumento>(tipoDocumento);
            return result;
        }

        public async Task<Result<List<Models.TipoDocumento>>> GetByFilter(FiltrosTipoDocumento filtros)
        {
            var result = new Result<List<Models.TipoDocumento>>(true, null, new List<Models.TipoDocumento>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var tipoDocumentos = await Repository.ListAsync(new TipoDocumentoSpec(filtros), ct.Token);
            result.Message = !tipoDocumentos.Any() ? "No se encontró tipo de documentos con la información proporcionada" : string.Empty;

            result.Entity = tipoDocumentos.Any() ? _mapper.Map<List<Models.TipoDocumento>>(tipoDocumentos) : result.Entity;
            return result;
        }

        public async Task<Result<Models.TipoDocumento>> Create(Models.TipoDocumento model, string userName)
        {
            var result = new Result<Models.TipoDocumento>(false, null, new Models.TipoDocumento());

            try
            {
                var validator = new RegistrarTipoDocumentoValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Models.TipoDocumento>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var tipoDocumento = await Repository.GetBySpecAsync(new TipoDocumentoSpec(new FiltrosTipoDocumento
                {
                    Nombre = model.Nombre
                }), ct.Token);

                if (tipoDocumento != null)
                {
                    result.Message = $"Ya existe un tipo de documento con el nombre {model.Nombre}";
                    return result;
                }

                var create = await Repository.AddAsync(_mapper.Map<QueryStack.TipoDocumento>(model),ct.Token);
                model.TipoDocumentoId = create.TipoDocumentoId;
                result.Entity = model;
                result.Success = true;
                result.Message = "Los datos se registraron con exito.";
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

        public async Task<Result<Models.TipoDocumento>> Update(Models.TipoDocumento model, string userName)
        {
            var result = new Result<Models.TipoDocumento>(false, null, new Models.TipoDocumento());
            try
            {
                var validator = new ActualizarTipoDocumentoValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<Models.TipoDocumento>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.TipoDocumento>(model));
                result.Entity = model;
                result.Success = true;
                result.Message = "Los datos se actualizaron con exito.";
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

        public async Task<Result<Models.TipoDocumento>> Delete(int id, string userName)
        {
            var result = new Result<Models.TipoDocumento>(false, null, new Models.TipoDocumento());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Success = true;
                result.Message = "Tipo de documento eliminado exitosamente";
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
    }
}
