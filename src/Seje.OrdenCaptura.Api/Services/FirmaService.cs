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
    public class FirmaService : IFirma
    {
        private readonly IMapper _mapper;
        private readonly ILogger<FirmaService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Firma> Repository { get; }

        public FirmaService(
        IMapper mapper,
        ILogger<FirmaService> logger,
        IMediator mediator,
        IRepository<QueryStack.Firma> repository
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
        }
        public async Task<Result<List<Firma>>> List()
        {
            var result = new Result<List<Firma>>(true, null, new List<Firma>());
            var firmas = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Firma>>(firmas);
            return result;
        }

        public async Task<Result<Firma>> GetById(int firmaId)
        {
            var result = new Result<Firma>(true, null, new Firma());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var firma = await Repository.GetByIdAsync(firmaId,ct.Token);

            if (firma == null)
            {
                result.Message = "No se encontró información de la firma";
                return result;
            }

            result.Entity = _mapper.Map<Firma>(firma);

            return result;
        }

        public async Task<Result<List<Firma>>> GetByFilter(FiltrosFirma filtros)
        {
            var result = new Result<List<Firma>>(true, null, new List<Firma>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var firmas = await Repository.ListAsync(new FirmaSpec(filtros), ct.Token);
            result.Message = !firmas.Any() ? "No se encontró firmas con la información proporcionada" : string.Empty;

            result.Entity = firmas.Any() ? _mapper.Map<List<Firma>>(firmas) : result.Entity;
            return result;
        }

        public async Task<Result<Firma>> Create(Firma model, string userName)
        {
            var result = new Result<Firma>(false, null, new Firma());

            try
            {
                var validator = new RegistrarFirmaValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<Firma>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var firma = await Repository.GetBySpecAsync(new FirmaSpec(new FiltrosFirma
                {
                    CorreoFirmante = model.CorreoFirmante,
                    NumeroOrdenCaptura = model.NumeroOrdenCaptura
                }), ct.Token);

                if (firma != null)
                {
                    result.Message = $"Ya existe una firma para {model.CorreoFirmante}";
                    return result;
                }

                var create = await Repository.AddAsync(_mapper.Map<QueryStack.Firma>(model),ct.Token);
                model.FirmaId = create.FirmaId;
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

        public async Task<Result<Firma>> Update(Firma model, string userName)
        {
            var result = new Result<Firma>(false, null, new Firma());
            try
            {
                var validator = new ActualizarFirmaValidator();
                var validation = validator.Validate(model);

                if (!validation.IsValid)
                    return Result<Firma>.Failure(validation?.Errors?.Select(e => e.ErrorMessage).FirstOrDefault());

                await Repository.UpdateAsync(_mapper.Map<QueryStack.Firma>(model));
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

        public async Task<Result<Firma>> Delete(int id, string userName)
        {
            var result = new Result<Firma>(false, null, new Firma());
            try
            {
                var model = await Repository.GetByIdAsync(id);
                await Repository.DeleteAsync(model);
                result.Success = true;
                result.Message = "Firma eliminada exitosamente";
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
