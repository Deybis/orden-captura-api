using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.SharedKernel.Results;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Controllers
{
    [Route("api/ordencapturaestado")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class OrdenCapturaEstadoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrdenCapturaEstado _ordenCapturaEstadoService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public OrdenCapturaEstadoController(IConfiguration configuration, IOrdenCapturaEstado ordenCapturaEstadoService)
        {
            _configuration = configuration;
            _ordenCapturaEstadoService = ordenCapturaEstadoService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(OrdenCapturaEstado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<OrdenCapturaEstado>>> List()
        {
            return await _ordenCapturaEstadoService.List();
        }

        [HttpGet("{ordenCapturaEstadoId}")]
        [ProducesResponseType(typeof(OrdenCapturaEstado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<OrdenCapturaEstado>> GetById(long ordenCapturaEstadoId)
        {
            return await _ordenCapturaEstadoService.GetById(ordenCapturaEstadoId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(OrdenCapturaEstado), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<OrdenCapturaEstado>>> GetByFilter([FromQuery] FiltrosOrdenCapturaEstado filtros)
        {
            return await _ordenCapturaEstadoService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<OrdenCapturaEstado>> Create(OrdenCapturaEstado estado)
        {
            var result = new Result<OrdenCapturaEstado>(false, null, new OrdenCapturaEstado());
            if (ModelState.IsValid)
            {
                result = await _ordenCapturaEstadoService.Create(estado, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<OrdenCapturaEstado>> Update(OrdenCapturaEstado estado)
        {
            var result = new Result<OrdenCapturaEstado>(false, null, new OrdenCapturaEstado());
            if (ModelState.IsValid)
            {
                result = await _ordenCapturaEstadoService.Update(estado, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<OrdenCapturaEstado>> Delete(long id)
        {
            var result = await _ordenCapturaEstadoService.Delete(id, UserName);
            return result;
        }
    }
}