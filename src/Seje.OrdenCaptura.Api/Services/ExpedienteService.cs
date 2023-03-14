using AutoMapper;
using Entities.Shared.Paging.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class ExpedienteService : IExpediente
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ExpedienteService> _logger;
        public IMediator Mediator { get; }
        public IRepository<QueryStack.Expediente> Repository { get; }
        private readonly IParte _parteService;
        private readonly IDelito _delitoService;
        public int OrganoJurisdiccional { get; set; }

        public ExpedienteService(
        IMapper mapper,
        ILogger<ExpedienteService> logger,
        IMediator mediator,
        IRepository<QueryStack.Expediente> repository,
        IHttpContextAccessor httpContextAccessor,
        IParte parteService,
        IDelito delitoService
        )
        {
            _mapper = mapper;
            _logger = logger;
            Mediator = mediator;
            Repository = repository;
            int.TryParse(httpContextAccessor.HttpContext.Request.Headers["organo_jurisdiccional_id"].FirstOrDefault(), out int value);
            OrganoJurisdiccional = value;
            _parteService = parteService;
            _delitoService = delitoService;
        }
        public async Task<Result<List<Expediente>>> List()
        {
            var result = new Result<List<Expediente>>(true, null, new List<Expediente>());
            var expedientes = await Repository.ListAsync();
            result.Entity = _mapper.Map<List<Expediente>>(expedientes);
            return result;
        }

        public async Task<PagedResult<Expediente>> List(FiltrosExpediente filtros)
        {
            PagedResult<Expediente> response = new();

            if (OrganoJurisdiccional == 0) return response;

            filtros.OrganoJurisdiccionalId = OrganoJurisdiccional;

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var expedientes = await Repository.ListAsync(new ExpedientePaginationSpec(filtros), ct.Token);

            var total = await Repository.ListAsync(new ExpedienteSpec(filtros));
            response.TotalCount = total.Count();

            response.Result = _mapper.Map<List<Expediente>>(expedientes.OrderByDescending(x => x.Id));
            response.PageNumber = filtros.PageNumber;
            response.PageSize = filtros.PageSize;
            return response;
        }

        public async Task<Result<Expediente>> GetById(long expedienteId)
        {
            var result = new Result<Expediente>(true, null, new Expediente());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var expediente = await Repository.GetByIdAsync(expedienteId, ct.Token);

            if (expediente == null)
            {
                result.Message = "No se encontró información del expediente";
                return result;
            }

            result.Entity = _mapper.Map<Expediente>(expediente);

            return result;
        }

        public async Task<Result<List<Expediente>>> GetByFilter(FiltrosExpediente filtros)
        {
            var result = new Result<List<Expediente>>(true, null, new List<Expediente>());

            var ct = new CancellationTokenSource();
            ct.CancelAfter(TimeSpan.FromSeconds(60));

            var expedientes = await Repository.ListAsync(new ExpedienteSpec(filtros), ct.Token);
            result.Message = !expedientes.Any() ? "No se encontró expedientes con la información proporcionada" : string.Empty;

            result.Entity = expedientes.Any() ? _mapper.Map<List<Expediente>>(expedientes.OrderByDescending(x => x.Id)) : result.Entity;
            return result;
        }

        public async Task<Result<RegistrarExpediente>> Create(RegistrarExpediente model, string userName)
        {
            var result = new Result<RegistrarExpediente>(true, null, new RegistrarExpediente());

            try
            {
                var validator = new RegistrarExpedienteValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<RegistrarExpediente>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));

                var expediente = await Repository.GetBySpecAsync(new ExpedienteSpec(new FiltrosExpediente
                {
                    NumeroExpediente = model.NumeroExpediente
                }), ct.Token);

                if (expediente != null)
                {
                    result.Message = "Ya existe un expediente con la información proporcionada";
                    result.Success = false;
                    return result;
                }
                var exp = _mapper.Map<QueryStack.Expediente>(model);
                exp.UsuarioCreacion = userName;
                exp.UsuarioModificacion = userName;
                exp.FechaCreacion = DateTime.Now;
                exp.FechaModificacion = DateTime.Now;

                foreach (var item in exp.Partes)
                {
                    item.UsuarioCreacion = userName;
                    item.UsuarioModificacion = userName;
                    item.FechaCreacion = DateTime.Now;
                    item.FechaModificacion = DateTime.Now;
                }

                foreach (var item in exp.Delitos)
                {
                    item.Codigo = Guid.NewGuid();
                    item.UsuarioCreacion = userName;
                    item.UsuarioModificacion = userName;
                    item.FechaCreacion = DateTime.Now;
                    item.FechaModificacion = DateTime.Now;
                }

                var create = await Repository.AddAsync(exp, ct.Token);
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

        public async Task<Result<ActualizarExpediente>> Update(ActualizarExpediente model, string userName)
        {
            var result = new Result<ActualizarExpediente>(true, null, new ActualizarExpediente());
            try
            {
                var validator = new ActualizarExpedienteValidator();
                var validation = validator.Validate(model);
                if (!validation.IsValid)
                    return Result<ActualizarExpediente>.Failure(validation.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

                var ct = new CancellationTokenSource();
                ct.CancelAfter(TimeSpan.FromSeconds(60));
                await Repository.SaveChangesAsync();

                var expedienteModificacion = _mapper.Map<QueryStack.Expediente>(model);
                expedienteModificacion.UsuarioCreacion = userName;
                expedienteModificacion.FechaCreacion = DateTime.Now;
                expedienteModificacion.UsuarioModificacion = userName;
                expedienteModificacion.FechaModificacion = DateTime.Now;

                foreach (var item in expedienteModificacion.Partes)
                {
                    item.UsuarioCreacion = userName;
                    item.FechaCreacion = DateTime.Now;
                    item.UsuarioModificacion = userName;
                    item.FechaModificacion = DateTime.Now;
                }

                foreach (var item in expedienteModificacion.Delitos)
                {
                    item.UsuarioCreacion = userName;
                    item.FechaCreacion = DateTime.Now;
                    item.UsuarioModificacion = userName;
                    item.FechaModificacion = DateTime.Now;
                }

                await Repository.UpdateAsync(expedienteModificacion, ct.Token);
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
    }
}
