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
    [Route("api/consulta")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ConsultaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IConsulta _consultaService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public ConsultaController(IConfiguration configuration, IConsulta consultaService)
        {
            _configuration = configuration;
            _consultaService = consultaService;
        }

        //[HttpGet("expediente/{numeroExpediente}")]
        //[ProducesResponseType(typeof(Expediente), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        //public async Task<Result<Seje.Expediente.Client.Models.Expediente>> GetExpediente(string numeroExpediente)
        //{
        //    return await _consultaService.GetExpediente(numeroExpediente);
        //}

        [HttpGet("estadisticas")]
        [ProducesResponseType(typeof(Expediente), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Models.Estadistica>> GetEstadisticas([FromQuery] FiltrosEstadistica filtros)
        {
            return await _consultaService.GetEstadisticas(filtros);
        }

        [HttpGet("ordencaptura")]
        [ProducesResponseType(typeof(Expediente), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<OrdenCaptura>>> ConsultaOrdenCaptura([FromQuery] FiltrosConsultaOrdenCaptura filtros)
        {
            return await _consultaService.ConsultaOrdenCaptura(filtros);
        }
    }
}