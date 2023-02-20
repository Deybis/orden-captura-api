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
    public class ParteService : IParte
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ParteService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Parte> Repository { get; }

        public ParteService(
        IMapper mapper,
        ILogger<ParteService> logger,
        IMediator mediator,
        IRepository<QueryStack.Parte> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<Parte>>> List()
        {
            var result = new Result<List<Parte>>(true, null, new List<Parte>());
            var partes = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Parte>>(partes);
            return result;
        }

        public async Task<Result<Parte>> GetById(long parteId)
        {
            var result = new Result<Parte>(true, null, new Parte());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var parte = await Repository.GetByIdAsync(parteId,ct.Token);

            if (parte == null)
            {
                result.Message = "No se encontró información de la parte";
                return result;
            }

            result.Entity = _mapper.Map<Parte>(parte);
            return result;
        }

        public async Task<Result<List<Parte>>> GetByFilter(FiltrosParte filtros)
        {
            var result = new Result<List<Parte>>(true, null, new List<Parte>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var partes = await Repository.ListAsync(new ParteSpec(filtros), ct.Token);
            result.Message = !partes.Any() ? "No se encontró partes con la información proporcionada" : string.Empty;

            result.Entity = partes.Any() ? _mapper.Map<List<Parte>>(partes) : result.Entity;
            return result;
        }

        public async Task<Result<Parte>> Create(RegistrarParte model, string userName)
        {
            var result = new Result<Parte>(true, null, new Parte());

            try
            {
                var validator = new RegistrarParteValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Parte>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var parte = await Repository.GetBySpecAsync(new ParteSpec(new FiltrosParte
                {
                    TipoParteId = model.TipoParteId,
                    Nombre = model.Nombre
                }), ct.Token);

                if (parte != null)
                {
                    result.Message = "Ya existe el típo de parte con la información proporcionada";
                    result.Success = false;
                    return result;
                }

                var create = Repository.AddAsync(_mapper.Map<QueryStack.Parte>(model));
                result.Entity = _mapper.Map<Parte>(create);
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

        public async Task<Result<Parte>> Update(ActualizarParte model, string userName)
        {
            var result = new Result<Parte>(true, null, new Parte());
            try
            {
                var validator = new ActualizarParteValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Parte>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                await Repository.UpdateAsync(_mapper.Map<QueryStack.Parte>(model), ct.Token);
                result.Entity = _mapper.Map<Parte>(model);
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

        public async Task<Result<Parte>> Delete(int id, string userName)
        {
            var result = new Result<Parte>(true, null, new Parte());
            try
            {
                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var model = await Repository.GetBySpecAsync(new ParteSpec(new FiltrosParte { ParteId = id}));
                await Repository.DeleteAsync(model,ct.Token);
                result.Success = true;
                result.Message = "Registro eliminado satisfactoriamente";
 
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
