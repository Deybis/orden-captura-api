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
    public class DelitoService : IDelito
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DelitoService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Delito> Repository { get; }

        public DelitoService(
        IMapper mapper,
        ILogger<DelitoService> logger,
        IMediator mediator,
        IRepository<QueryStack.Delito> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<Delito>>> List()
        {
            var result = new Result<List<Delito>>(true, null, new List<Delito>());
            var delitos = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Delito>>(delitos);
            return result;
        }

        public async Task<Result<Delito>> GetById(long delitoId)
        {
            var result = new Result<Delito>(true, null, new Delito());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var delito = await Repository.GetByIdAsync(delitoId,ct.Token);

            if (delito == null)
            {
                result.Message = "No se encontró información del delito";
                return result;
            }

            result.Entity = _mapper.Map<Delito>(delito);

            return result;
        }

        public async Task<Result<List<Delito>>> GetByFilter(FiltrosDelito filtros)
        {
            var result = new Result<List<Delito>>(true, null, new List<Delito>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var delitos = await Repository.ListAsync(new DelitoSpec(filtros), ct.Token);
            result.Message = !delitos.Any() ? "No se encontró delitos con la información proporcionada" : string.Empty;

            result.Entity = delitos.Any() ? _mapper.Map<List<Delito>>(delitos) : result.Entity;
            return result;
        }

        public async Task<Result<Delito>> Create(RegistrarDelito model, string userName)
        {
            var result = new Result<Delito>(true, null, new Delito());

            try
            {
                var validator = new RegistrarDelitoValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Delito>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var delito = await Repository.GetBySpecAsync(new DelitoSpec(new FiltrosDelito
                {
                    Nombre = model.Nombre
                }), ct.Token);

                if (delito != null)
                {
                    result.Message = "Ya existe un delito con la información proporcionada";
                    return result;
                }

                var create = Repository.AddAsync(_mapper.Map<QueryStack.Delito>(model),ct.Token);
                result.Entity = _mapper.Map<Delito>(create);
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

        public async Task<Result<Delito>> Update(ActualizarDelito model, string userName)
        {
            var result = new Result<Delito>(true, null, new Delito());
            try
            {
                var validator = new ActualizarDelitoValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<Delito>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.Delito>(model));
                result.Entity = _mapper.Map<Delito>(model);
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

        public async Task<Result<Delito>> Delete(long id, string userName)
        {
            var result = new Result<Delito>(true, null, new Delito());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Message = "Delito eliminado exitosamente";
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
