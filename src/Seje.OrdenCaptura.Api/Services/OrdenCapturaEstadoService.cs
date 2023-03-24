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
    public class OrdenCapturaEstadoService : IOrdenCapturaEstado
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrdenCapturaEstado> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.OrdenCapturaEstado> Repository { get; }

        public OrdenCapturaEstadoService(
        IMapper mapper,
        ILogger<OrdenCapturaEstado> logger,
        IMediator mediator,
        IRepository<QueryStack.OrdenCapturaEstado> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<OrdenCapturaEstado>>> List()
        {
            var result = new Result<List<OrdenCapturaEstado>>(true, null, new List<OrdenCapturaEstado>());
            var estados = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<OrdenCapturaEstado>>(estados);
            return result;
        }

        public async Task<Result<OrdenCapturaEstado>> GetById(long id)
        {
            var result = new Result<OrdenCapturaEstado>(true, null, new OrdenCapturaEstado());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var estado = await Repository.GetByIdAsync(id,ct.Token);

            if (estado == null)
            {
                result.Message = "No se encontró información del estado";
                return result;
            }

            result.Entity = _mapper.Map<OrdenCapturaEstado>(estado);

            return result;
        }

        public async Task<Result<List<OrdenCapturaEstado>>> GetByFilter(FiltrosOrdenCapturaEstado filtros)
        {
            var result = new Result<List<OrdenCapturaEstado>>(true, null, new List<OrdenCapturaEstado>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var estados = await Repository.ListAsync(new OrdenCapturaEstadoSpec(filtros), ct.Token);
            result.Message = !estados.Any() ? "No se encontró estado con la información proporcionada" : string.Empty;

            result.Entity = estados.Any() ? _mapper.Map<List<OrdenCapturaEstado>>(estados) : result.Entity;
            return result;
        }

        public async Task<Result<OrdenCapturaEstado>> Create(OrdenCapturaEstado model, string userName)
        {
            var result = new Result<OrdenCapturaEstado>(true, null, new OrdenCapturaEstado());

            try
            {
                var validator = new RegistrarOrdenCapturaEstadoValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<OrdenCapturaEstado>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var estado = await Repository.GetBySpecAsync(new OrdenCapturaEstadoSpec(new FiltrosOrdenCapturaEstado
                {
                    Descripcion = model.Descripcion
                }), ct.Token);

                if (estado != null)
                {
                    result.Message = "Ya existe un estado con la información proporcionada";
                    return result;
                }

                var create = Repository.AddAsync(_mapper.Map<QueryStack.OrdenCapturaEstado>(model),ct.Token);
                result.Entity = _mapper.Map<OrdenCapturaEstado>(create);
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

        public async Task<Result<OrdenCapturaEstado>> Update(OrdenCapturaEstado model, string userName)
        {
            var result = new Result<OrdenCapturaEstado>(true, null, new OrdenCapturaEstado());
            try
            {
                var validator = new ActualizarOrdenCapturaEstadoValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<OrdenCapturaEstado>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.OrdenCapturaEstado>(model));
                result.Entity = _mapper.Map<OrdenCapturaEstado>(model);
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

        public async Task<Result<OrdenCapturaEstado>> Delete(long id, string userName)
        {
            var result = new Result<OrdenCapturaEstado>(true, null, new OrdenCapturaEstado());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Message = "Estado eliminado exitosamente";
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
