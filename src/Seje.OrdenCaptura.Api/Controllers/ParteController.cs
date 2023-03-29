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
    [Route("api/parte")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ParteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IParte _parteService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public ParteController(IConfiguration configuration, IParte parteService)
        {
            _configuration = configuration;
            _parteService = parteService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Result<List<Parte>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Parte>>> List()
        {
            return await _parteService.List();
        }

        [HttpGet("{parteId}")]
        [ProducesResponseType(typeof(Result<Parte>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Parte>> GetById(long delitoId)
        {
            return await _parteService.GetById(delitoId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Result<List<Parte>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Parte>>> GetByFilter([FromQuery] FiltrosParte filtros)
        {
            return await _parteService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Result<Parte>), (int)HttpStatusCode.OK)]
        public async Task<Result<Parte>> Create(RegistrarParte delito)
        {
            var result = new Result<Parte>(true, null, new Parte());
            if (ModelState.IsValid)
            {
                result = await _parteService.Create(delito, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(Result<Parte>), (int)HttpStatusCode.OK)]
        public async Task<Result<Parte>> Update(ActualizarParte delito)
        {
            var result = new Result<Parte>(true, null, new Parte());
            if (ModelState.IsValid)
            {
                result = await _parteService.Update(delito, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(Result<Parte>), (int)HttpStatusCode.OK)]
        public async Task<Result<Parte>> Delete(int parteId)
        {
            var result = await _parteService.Delete(parteId, UserName);
            return result;
        }
    }
}
