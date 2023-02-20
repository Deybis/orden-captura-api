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
    [Route("api/tipodocumento")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITipoDocumento _tipoDocumentoService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public TipoDocumentoController(IConfiguration configuration, ITipoDocumento tipoDocumentoService)
        {
            _configuration = configuration;
            _tipoDocumentoService = tipoDocumentoService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Models.TipoDocumento), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Models.TipoDocumento>>> List()
        {
            return await _tipoDocumentoService.List();
        }

        [HttpGet("{tipodocumentoId}")]
        [ProducesResponseType(typeof(Models.TipoDocumento), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Models.TipoDocumento>> GetById(int tipoDocumentoId)
        {
            return await _tipoDocumentoService.GetById(tipoDocumentoId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Models.TipoDocumento), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Models.TipoDocumento>>> GetByFilter([FromQuery] FiltrosTipoDocumento filtros)
        {
            return await _tipoDocumentoService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Models.TipoDocumento>> Create(Models.TipoDocumento documento)
        {
            var result = new Result<Models.TipoDocumento>(true, null, new Models.TipoDocumento());
            if (ModelState.IsValid)
            {
                result = await _tipoDocumentoService.Create(documento, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Models.TipoDocumento>> Update(Models.TipoDocumento tipoDocumento)
        {
            var result = new Result<Models.TipoDocumento>(true, null, new Models.TipoDocumento());
            if (ModelState.IsValid)
            {
                result = await _tipoDocumentoService.Update(tipoDocumento, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Models.TipoDocumento>> Delete(int id)
        {
            var result = await _tipoDocumentoService.Delete(id, UserName);
            return result;
        }
    }
}