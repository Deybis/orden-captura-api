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
    [Route("api/configuracion")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ConfiguracionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConfiguracion _configuracionService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public ConfiguracionController(IConfiguration configuration, IConfiguracion configuracionService)
        {
            _configuration = configuration;
            _configuracionService = configuracionService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Result<List<Configuracion>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Configuracion>>> List()
        {
            return await _configuracionService.List();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Result<Configuracion>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Configuracion>> GetById(int id)
        {
            return await _configuracionService.GetById(id);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Result<List<Configuracion>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Configuracion>>> GetByFilter([FromQuery] FiltrosConfiguracion filtros)
        {
            return await _configuracionService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Result<Configuracion>), (int)HttpStatusCode.OK)]
        public async Task<Result<Configuracion>> Create(Configuracion delito)
        {
            var result = new Result<Configuracion>(false, null, new Configuracion());
            if (ModelState.IsValid)
            {
                result = await _configuracionService.Create(delito, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(Result<Configuracion>), (int)HttpStatusCode.OK)]
        public async Task<Result<Configuracion>> Update(Configuracion delito)
        {
            var result = new Result<Configuracion>(false, null, new Configuracion());
            if (ModelState.IsValid)
            {
                result = await _configuracionService.Update(delito, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(Result<Configuracion>), (int)HttpStatusCode.OK)]
        public async Task<Result<Configuracion>> Delete(int id)
        {
            var result = await _configuracionService.Delete(id, UserName);
            return result;
        }
    }
}