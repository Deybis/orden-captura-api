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
    public class TipoFirmaService : ITipoFirma
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TipoFirmaService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.TipoFirma> Repository { get; }

        public TipoFirmaService(
        IMapper mapper,
        ILogger<TipoFirmaService> logger,
        IMediator mediator,
        IRepository<QueryStack.TipoFirma> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<TipoFirma>>> List()
        {
            var result = new Result<List<TipoFirma>>(true, null, new List<TipoFirma>());
            var tipofirmas = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<TipoFirma>>(tipofirmas);
            return result;
        }

        public async Task<Result<TipoFirma>> GetById(int tipoFirmaId)
        {
            var result = new Result<TipoFirma>(true, null, new TipoFirma());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var tipoFirma = await Repository.GetByIdAsync(tipoFirmaId, ct.Token);

            if (tipoFirma == null)
            {
                result.Message = "No se encontró información del tipo de firma";
                return result;
            }
            result.Entity = _mapper.Map<TipoFirma>(tipoFirma);
            return result;
        }

        public async Task<Result<List<TipoFirma>>> GetByFilter(FiltrosTipoFirma filtros)
        {
            var result = new Result<List<TipoFirma>>(true, null, new List<TipoFirma>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var tipoFirmas = await Repository.ListAsync(new TipoFirmaSpec(filtros), ct.Token);
            result.Message = !tipoFirmas.Any() ? "No se encontró tipo de firmas con la información proporcionada" : string.Empty;

            result.Entity = tipoFirmas.Any() ? _mapper.Map<List<TipoFirma>>(tipoFirmas) : result.Entity;
            return result;
        }

        public async Task<Result<TipoFirma>> Create(TipoFirma model, string userName)
        {
            var result = new Result<TipoFirma>(false, null, new TipoFirma());

            try
            {
                var validator = new RegistrarTipoFirmaValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<TipoFirma>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var tipoFirma = await Repository.GetBySpecAsync(new TipoFirmaSpec(new FiltrosTipoFirma
                {
                    Nombre = model.Nombre
                }), ct.Token);

                if (tipoFirma != null)
                {
                    result.Message = $"Ya existe un tipo de firma con el nombre {model.Nombre}";
                    return result;
                }

                var create = await Repository.AddAsync(_mapper.Map<QueryStack.TipoFirma>(model),ct.Token);
                model.TipoFirmaId = create.TipoFirmaId;
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

        public async Task<Result<TipoFirma>> Update(TipoFirma model, string userName)
        {
            var result = new Result<TipoFirma>(false, null, new TipoFirma());
            try
            {
                var validator = new ActualizarTipoFirmaValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<TipoFirma>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.TipoFirma>(model));
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

        public async Task<Result<TipoFirma>> Delete(int id, string userName)
        {
            var result = new Result<TipoFirma>(false, null, new TipoFirma());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Success = true;
                result.Message = "Tipo de firma eliminado exitosamente";
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
