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
    [Route("api/firma")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class FirmaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFirma _firmaService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public FirmaController(IConfiguration configuration, IFirma firmaService)
        {
            _configuration = configuration;
            _firmaService = firmaService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Firma), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Firma>>> List()
        {
            return await _firmaService.List();
        }

        [HttpGet("{firmaId}")]
        [ProducesResponseType(typeof(Firma), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Firma>> GetById(int firmaId)
        {
            return await _firmaService.GetById(firmaId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Firma), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Firma>>> GetByFilter([FromQuery] FiltrosFirma filtros)
        {
            return await _firmaService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Firma>> Create(Firma firma)
        {
            var result = new Result<Firma>(false, null, new Firma());
            if (ModelState.IsValid)
            {
                result = await _firmaService.Create(firma, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Firma>> Update(Firma firma)
        {
            var result = new Result<Firma>(false, null, new Firma());
            if (ModelState.IsValid)
            {
                result = await _firmaService.Update(firma, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Result<Firma>> Delete(int id)
        {
            var result = await _firmaService.Delete(id, UserName);
            return result;
        }
    }
}