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
    public class ConfiguracionService : IConfiguracion
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ConfiguracionService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Configuracion> Repository { get; }

        public ConfiguracionService(
        IMapper mapper,
        ILogger<ConfiguracionService> logger,
        IMediator mediator,
        IRepository<QueryStack.Configuracion> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<Configuracion>>> List()
        {
            var result = new Result<List<Configuracion>>(true, null, new List<Configuracion>());
            var configuraciones = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Configuracion>>(configuraciones);
            return result;
        }

        public async Task<Result<Configuracion>> GetById(int configuracionId)
        {
            var result = new Result<Configuracion>(true, null, new Configuracion());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var configuracion = await Repository.GetByIdAsync(configuracionId, ct.Token);

            if (configuracion == null)
            {
                result.Message = "No se encontró información de la configuración";
                return result;
            }

            result.Entity = _mapper.Map<Configuracion>(configuracion);
            return result;
        }

        public async Task<Result<List<Configuracion>>> GetByFilter(FiltrosConfiguracion filtros)
        {
            var result = new Result<List<Configuracion>>(true, null, new List<Configuracion>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var configuraciones = await Repository.ListAsync(new ConfiguracionSpec(filtros), ct.Token);
            result.Message = !configuraciones.Any() ? "No se encontró configuraciones con la información proporcionada" : string.Empty;

            result.Entity = configuraciones.Any() ? _mapper.Map<List<Configuracion>>(configuraciones) : result.Entity;
            return result;
        }

        public async Task<Result<Configuracion>> Create(Configuracion model, string userName)
        {
            var result = new Result<Configuracion>(false, null, new Configuracion());

            try
            {
                var validator = new RegistrarConfiguracionValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Configuracion>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var configuracion = await Repository.GetBySpecAsync(new ConfiguracionSpec(new FiltrosConfiguracion
                {
                    Nombre = model.Nombre
                }), ct.Token);

                if (configuracion != null)
                {
                    result.Message = $"Ya existe una configuracion con el nombre {model.Nombre}";
                    return result;
                }

                var create = Repository.AddAsync(_mapper.Map<QueryStack.Configuracion>(model),ct.Token);
                result.Entity = _mapper.Map<Configuracion>(create);
                result.Success = true;
                result.Message = "Los datos se registrarón con exito.";
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

        public async Task<Result<Configuracion>> Update(Configuracion model, string userName)
        {
            var result = new Result<Configuracion>(false, null, new Configuracion());
            try
            {
                var validator = new ActualizarConfiguracionValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<Configuracion>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.Configuracion>(model));
                result.Entity = _mapper.Map<Configuracion>(model);
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

        public async Task<Result<Configuracion>> Delete(int id, string userName)
        {
            var result = new Result<Configuracion>(false, null, new Configuracion());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Success = true;
                result.Message = "Configuración eliminada exitosamente";
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
