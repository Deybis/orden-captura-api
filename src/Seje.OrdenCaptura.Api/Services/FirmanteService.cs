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
    public class FirmanteService : IFirmante
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FirmanteService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Firmante> Repository { get; }

        public FirmanteService(
        IMapper mapper,
        ILogger<FirmanteService> logger,
        IMediator mediator,
        IRepository<QueryStack.Firmante> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<Firmante>>> List()
        {
            var result = new Result<List<Firmante>>(true, null, new List<Firmante>());
            var firmantes = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Firmante>>(firmantes);
            return result;
        }

        public async Task<Result<Firmante>> GetById(int firmanteId)
        {
            var result = new Result<Firmante>(true, null, new Firmante());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var firmante = await Repository.GetByIdAsync(firmanteId,ct.Token);

            if (firmante == null)
            {
                result.Message = "No se encontró información del firmante";
                return result;
            }

            result.Entity = _mapper.Map<Firmante>(firmante);

            return result;
        }

        public async Task<Result<List<Firmante>>> GetByFilter(FiltrosFirmante filtros)
        {
            var result = new Result<List<Firmante>>(true, null, new List<Firmante>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var firmantes = await Repository.ListAsync(new FirmanteSpec(filtros), ct.Token);
            result.Message = !firmantes.Any() ? "No se encontró firmantess con la información proporcionada" : string.Empty;

            result.Entity = firmantes.Any() ? _mapper.Map<List<Firmante>>(firmantes) : result.Entity;
            return result;
        }

        public async Task<Result<Firmante>> Create(Firmante model, string userName)
        {
            var result = new Result<Firmante>(false, null, new Firmante());

            try
            {
                var validator = new RegistrarFirmanteValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Firmante>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var firmante = await Repository.GetBySpecAsync(new FirmanteSpec(new FiltrosFirmante
                {
                    Identificador = model.Identificador
                }), ct.Token);

                if (firmante != null)
                {
                    result.Message = $"Ya existe un firmante con el identificador {model.Identificador}";
                    return result;
                }

                var create = await Repository.AddAsync(_mapper.Map<QueryStack.Firmante>(model),ct.Token);
                model.FirmanteId = create.FirmanteId;
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

        public async Task<Result<Firmante>> Update(Firmante model, string userName)
        {
            var result = new Result<Firmante>(false, null, new Firmante());
            try
            {
                var validator = new ActualizarFirmanteValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<Firmante>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.Firmante>(model));
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

        public async Task<Result<Firmante>> Delete(int id, string userName)
        {
            var result = new Result<Firmante>(false, null, new Firmante());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Success = true;
                result.Message = "Firmante eliminado exitosamente";
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
