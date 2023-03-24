using AutoMapper;
using Microsoft.Extensions.Logging;
using Seje.Expediente.Client;
using Seje.OrdenCaptura.Api.Infrastructure.Interfaces;
using Seje.OrdenCaptura.Api.Models;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Services
{
    public class ConsultaService : IConsulta
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ConsultaService> _logger;
        private readonly IExpedienteService _expedienteClient;
        private readonly IOrdenCaptura _ordenCapturaService;

        public ConsultaService(
        IMapper mapper,
        ILogger<ConsultaService> logger,
        IExpedienteService expedienteClient,
        IOrdenCaptura ordenCapturaService)
        {
            _mapper = mapper;
            _logger = logger;
            _expedienteClient = expedienteClient;
            _ordenCapturaService = ordenCapturaService;
        }

        public async Task<Result<Estadistica>> GetEstadisticas(FiltrosEstadistica filtros)
        {
            var result = new Result<Estadistica>(false, null, new());
            try
            {
                var ordenesCaptura = await _ordenCapturaService.GetByFilter(new FiltrosOrdenCaptura {
                   OrganoJurisdiccionalId = filtros.OrganoJurisdiccionalId,
                   AñoActual = filtros.Año == 0 ? string.Empty : filtros.Año.ToString(),
                   Mes = filtros.Mes
                });

                result.Entity.TotalBorrador = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.Borrador).ToList().Count;
                result.Entity.TotalRechazadas = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.Rechazada).ToList().Count;
                result.Entity.TotalEnRevision = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.EnRevision).ToList().Count;
                result.Entity.TotalPendienteDeFirma = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.PendienteDeFirma).ToList().Count;
                result.Entity.TotalPendienteDeEntrega = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.PendienteDeEntrega).ToList().Count;
                result.Entity.TotalActivas = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.Activa).ToList().Count;
                result.Entity.TotalContraCapturaPendienteDeFirma = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.ContraCapturaPendienteDeFirma).ToList().Count;
                result.Entity.TotalContraCaptura = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.ContraCaptura).ToList().Count;
                result.Entity.TotalEjecutadas = ordenesCaptura?.Entity?.Where(x => x.OrdenCapturaEstadoId == (int)OrdenCapturaEstados.Ejecutada).ToList().Count;
                result.Entity.Total = ordenesCaptura?.Entity?.Count;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Ocurrio un error al intentar obtener estadisticas:{ex.Message}");
                return result;
            }
        }

        public async Task<Result<Seje.Expediente.Client.Models.Expediente>> GetExpediente(string numeroExpediente)
        {
            var result = new Result<Seje.Expediente.Client.Models.Expediente>(false, null, new Seje.Expediente.Client.Models.Expediente());
            try
            {
                var response = await _expedienteClient.GetExpediente(numeroExpediente);
                result.Entity = response;
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return result;
            }
        }
    }
}
