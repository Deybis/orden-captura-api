using AutoMapper;
using MementoFX.Persistence;
using Microsoft.Extensions.Logging;
using Seje.Expediente.Client;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.Api.Models;
using Seje.OrdenCaptura.QueryStack;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Services
{
    public class ConsultaService : IConsulta
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ConsultaService> _logger;
        private IRepository<QueryStack.OrdenCaptura> OrdenCapturaRepository;
        private readonly IParte _parteService;

        public ConsultaService(
        IMapper mapper,
        ILogger<ConsultaService> logger,
        IRepository<QueryStack.OrdenCaptura> ordenCapturaRepository,
        IParte parteService)
        {
            _mapper = mapper;
            _logger = logger;
            OrdenCapturaRepository = ordenCapturaRepository;
            _parteService = parteService;
        }

        public async Task<Result<Estadistica>> GetEstadisticas(FiltrosEstadistica filtros)
        {
            var result = new Result<Estadistica>(false, null, new());
            try
            {
                var ordenesCaptura = await OrdenCapturaRepository.ListAsync(new OrdenCapturaSpec(new FiltrosOrdenCaptura
                {
                    OrganoJurisdiccionalId = filtros.OrganoJurisdiccionalId,
                    AñoActual = filtros.Año == 0 ? string.Empty : filtros.Año.ToString(),
                    Mes = filtros.Mes
                }));

                List<OrdenCaptura> oc = _mapper.Map<List<OrdenCaptura>>(ordenesCaptura);
                List<EstadisticaEstado> estados = oc.GroupBy(p => p.OrdenCapturaEstadoDescripcion,(key, g) => new EstadisticaEstado{ Estado = key, Total = g.ToList().Count }).ToList();
                result.Entity.Estados = estados;
                result.Entity.TotalOrdenesCaptura = oc.Count;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ocurrio un error al intentar obtener estadisticas:{ex.Message}");
                return result;
            }
        }

        public async Task<Result<List<OrdenCaptura>>> ConsultaOrdenCaptura(FiltrosConsultaOrdenCaptura filtros)
        {
            Result<List<OrdenCaptura>> result = new(false, null, new List<OrdenCaptura>());
            try
            {
                var ordenesCaptura = await OrdenCapturaRepository.ListAsync(new OrdenCapturaSpec(new FiltrosOrdenCaptura
                {
                    NumeroOrdenCaptura = filtros.Campo == "numeroOrdenCaptura" ? filtros.Valor : string.Empty,
                    NumeroExpediente = filtros.Campo == " numeroExpediente" ? filtros.Valor : string.Empty,
                    NombreImputado = filtros.Campo == "nombre" ? filtros.Valor : string.Empty
                }));

                result.Entity = _mapper.Map<List<OrdenCaptura>>(ordenesCaptura);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
