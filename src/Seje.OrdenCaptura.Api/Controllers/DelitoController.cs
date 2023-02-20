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
    [Route("api/delito")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DelitoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDelito _delitoService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public DelitoController(IConfiguration configuration, IDelito delitoService)
        {
            _configuration = configuration;
            _delitoService = delitoService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Delito), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Delito>>> List()
        {
            return await _delitoService.List();
        }

        [HttpGet("{delitoId}")]
        [ProducesResponseType(typeof(Delito), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Delito>> GetById(long delitoId)
        {
            return await _delitoService.GetById(delitoId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Delito), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Delito>>> GetByFilter([FromQuery] FiltrosDelito filtros)
        {
            return await _delitoService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Delito>> Create(RegistrarDelito delito)
        {
            var result = new Result<Delito>(true, null, new Delito());
            if (ModelState.IsValid)
            {
                result = await _delitoService.Create(delito, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Delito>> Update(ActualizarDelito delito)
        {
            var result = new Result<Delito>(true, null, new Delito());
            if (ModelState.IsValid)
            {
                result = await _delitoService.Update(delito, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Delito>> Delete(long id)
        {
            var result = await _delitoService.Delete(id, UserName);
            return result;
        }
    }
}