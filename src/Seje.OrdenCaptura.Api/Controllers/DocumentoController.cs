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
    [Route("api/documento")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class DocumentoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDocumento _documentoService;
        private string UserName { get { return User.Identity.Name ?? _configuration.GetValue<string>("UserAnonymous"); } }

        public DocumentoController(IConfiguration configuration, IDocumento documentoService)
        {
            _configuration = configuration;
            _documentoService = documentoService;
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(Result<List<Documento>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Documento>>> List()
        {
            return await _documentoService.List();
        }

        [HttpGet("{documentoId}")]
        [ProducesResponseType(typeof(Result<Documento>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<Documento>> GetById(long documentoId)
        {
            return await _documentoService.GetById(documentoId);
        }

        [HttpGet("byfilter")]
        [ProducesResponseType(typeof(Result<List<Documento>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        public async Task<Result<List<Documento>>> GetByFilter([FromQuery] FiltrosDocumento filtros)
        {
            return await _documentoService.GetByFilter(filtros);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Result<List<RegistrarDocumento>>), (int)HttpStatusCode.OK)]
        public async Task<Result<List<RegistrarDocumento>>> Create(List<RegistrarDocumento> files)
        {
            var result = new Result<List<RegistrarDocumento>>(false, null, new List<RegistrarDocumento>());
            if (ModelState.IsValid)
            {
                result = await _documentoService.CreateMultiple(files, UserName);
            }
            return result;
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(Result<Documento>), (int)HttpStatusCode.OK)]
        public async Task<Result<Documento>> Update(Documento documento)
        {
            var result = new Result<Documento>(true, null, new Documento());
            if (ModelState.IsValid)
            {
                result = await _documentoService.Update(documento, UserName);
            }
            return result;
        }

        [HttpDelete("delete")]
        [ProducesResponseType(typeof(Result<Documento>), (int)HttpStatusCode.OK)]
        public async Task<Result<Documento>> Delete(long id)
        {
            var result = await _documentoService.Delete(id, UserName);
            return result;
        }
    }
}