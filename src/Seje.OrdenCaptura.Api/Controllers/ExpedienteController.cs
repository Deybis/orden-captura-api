using Entities.Shared.Paging.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Controllers
{
    [Route("api/expediente")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ExpedienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IExpediente _expedienteService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public ExpedienteController(IConfiguration configuration, IExpediente expedienteService)
        {
            _configuration = configuration;
            _expedienteService = expedienteService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Expediente), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Expediente>>> List()
        {
            return await _expedienteService.List();
        }

        [HttpGet("paginatedlist")]
        [ProducesResponseType(typeof(Expediente), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<PagedResult<Expediente>> List([FromQuery] FiltrosExpediente filtros)
        {
            return await _expedienteService.List(filtros);
        }

        [HttpGet("{expedienteId}")]
        [ProducesResponseType(typeof(OrdenCaptura), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Expediente>> GetById(long expedienteId)
        {
            return await _expedienteService.GetById(expedienteId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Expediente), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Expediente>>> GetByFilter([FromQuery] FiltrosExpediente filtros)
        {
            return await _expedienteService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<RegistrarExpediente>> Create(RegistrarExpediente expediente)
        {
            var result = new Result<RegistrarExpediente>(true, null, new RegistrarExpediente());
            if (ModelState.IsValid)
            {
                result = await _expedienteService.Create(expediente, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<ActualizarExpediente>> Update(ActualizarExpediente expediente)
        {
            var result = new Result<ActualizarExpediente>(true, null, new ActualizarExpediente());
            if (ModelState.IsValid)
            {
                result = await _expedienteService.Update(expediente, UserName);
            }
            return result;
        }
    }
}