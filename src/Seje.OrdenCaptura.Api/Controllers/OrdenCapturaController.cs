using Entities.Shared.Model;
using Entities.Shared.Paging.Generic;
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
    [Route("api/ordencaptura")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class OrdenCapturaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOrdenCaptura _ordenCapturaService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public OrdenCapturaController(IConfiguration configuration,IOrdenCaptura ordenCapturaService)
        {
            _configuration = configuration;
            _ordenCapturaService = ordenCapturaService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(OrdenCaptura), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<OrdenCaptura>>> List()
        {
            return await _ordenCapturaService.List();
        }

        [HttpGet("paginatedlist")]
        [ProducesResponseType(typeof(OrdenCaptura), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<PagedResult<OrdenCaptura>> List([FromQuery] FiltrosOrdenCaptura filtros)
        {
            return await _ordenCapturaService.List(filtros, UserName);
        }

        [HttpGet("{ordenCapturaId}")]
        [ProducesResponseType(typeof(OrdenCaptura), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<OrdenCaptura>> GetById(long ordenCapturaId)
        {
            return await _ordenCapturaService.GetById(ordenCapturaId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(OrdenCaptura), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<OrdenCaptura>>> GetByFilter([FromQuery] FiltrosOrdenCaptura filtros)
        {
            return await _ordenCapturaService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<OrdenCaptura>> Create(RegistrarOrdenCaptura ordenCaptura)
        {
            var result = new Result<OrdenCaptura>(true, null, new OrdenCaptura());
            if (ModelState.IsValid)
            {
                result = await _ordenCapturaService.Create(ordenCaptura, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<OrdenCaptura>> Update(ActualizarOrdenCaptura ordenCaptura)
        {
            var result = new Result<OrdenCaptura>(true, null, new OrdenCaptura());
            if (ModelState.IsValid)
            {
                result = await _ordenCapturaService.Update(ordenCaptura, UserName);
            }
            return result;
        }

        [HttpPost("agregarfirmas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<List<Firma>>> AgregarFirmas(AgregarFirmas model)
        {
            var result = new Result<List<Firma>>(false, null, new List<Firma>());
            if (ModelState.IsValid)
            {
                result = await _ordenCapturaService.AgregarFirmas(model.NumeroOrdenCaptura, model.NumeroFirmas, UserName);
            }
            return result;
        }

        [HttpPost("firmar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<FirmaResponse>> Firmar(FirmaRequest request)
        {
            var result = new Result<FirmaResponse>(false, null, new FirmaResponse());
            if (ModelState.IsValid)
            {
                result = await _ordenCapturaService.Firmar(request,UserName);
            }
            return result;
        }

    }
}
