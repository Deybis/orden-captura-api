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
    [Route("api/firmante")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FirmanteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFirmante _firmanteService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public FirmanteController(IConfiguration configuration, IFirmante firmanteService)
        {
            _configuration = configuration;
            _firmanteService = firmanteService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Firmante), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Firmante>>> List()
        {
            return await _firmanteService.List();
        }

        [HttpGet("{firmanteId}")]
        [ProducesResponseType(typeof(Firmante), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Firmante>> GetById(int firmanteId)
        {
            return await _firmanteService.GetById(firmanteId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Firmante), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Firmante>>> GetByFilter([FromQuery] FiltrosFirmante filtros)
        {
            return await _firmanteService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Firmante>> Create(Firmante firma)
        {
            var result = new Result<Firmante>(true, null, new Firmante());
            if (ModelState.IsValid)
            {
                result = await _firmanteService.Create(firma, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Firmante>> Update(Firmante firma)
        {
            var result = new Result<Firmante>(true, null, new Firmante());
            if (ModelState.IsValid)
            {
                result = await _firmanteService.Update(firma, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Firmante>> Delete(int id)
        {
            var result = await _firmanteService.Delete(id, UserName);
            return result;
        }
    }
}