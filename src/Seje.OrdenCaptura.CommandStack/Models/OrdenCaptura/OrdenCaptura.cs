using MementoFX.Domain;
using System;
using System.Collections.Generic;

namespace Seje.OrdenCaptura.CommandStack.Models
{
    public partial class OrdenCaptura : Aggregate
    {
        public long OrdenCapturaId { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public Guid OrdenCapturaCodigo { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public int Correlativo { get; set; }
        public int OrdenCapturaEstadoId { get; set; }
        public string OrdenCapturaEstadoDescripcion { get; set; }
        public long ExpedienteId { get; set; }
        public string NumeroExpediente { get; set; }
        public int InstanciaId { get; set; }
        public string InstanciaDescripcion { get; set; }
        public string CorreoSecretario { get; set; }
        public string CorreoJuez { get; set; }
        public string CorreoEscribiente { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool AlertaInternacional { get; set; }
        public int DepartamentoId { get; set; }
        public string DepartamentoDescripcion { get; set; }
        public int MunicipioId { get; set; }
        public string MunicipioDescripcion { get; set; }
        public string Observaciones { get; set; }
        public IEnumerable<MementoFX.DomainEvent> GetOccurredEvents()
        {
            return OccurredEvents;
        }
    }
}
