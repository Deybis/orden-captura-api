using MementoFX.Domain;
using Seje.OrdenCaptura.CommandStack.Events;

namespace Seje.OrdenCaptura.CommandStack.Models
{
    public partial class OrdenCaptura : 
        IApplyEvent<OrdenCapturaRegistradaEvent>,
        IApplyEvent<OrdenCapturaModificadaEvent>
    {
        public void ApplyEvent(OrdenCapturaRegistradaEvent @event)
        {
            this.OrdenCapturaId = OrdenCapturaId;
            this.OrganoJurisdiccionalId = OrganoJurisdiccionalId;
            this.OrganoJurisdiccionalDescripcion = OrganoJurisdiccionalDescripcion;
            this.OrdenCapturaCodigo = OrdenCapturaCodigo;
            this.NumeroOrdenCaptura = NumeroOrdenCaptura;
            this.Correlativo = Correlativo;
            this.OrdenCapturaEstadoId = OrdenCapturaEstadoId;
            this.OrdenCapturaEstadoDescripcion = OrdenCapturaEstadoDescripcion;
            this.ExpedienteId = ExpedienteId;
            this.NumeroExpediente = NumeroExpediente;
            this.InstanciaId = InstanciaId;
            this.InstanciaDescripcion = InstanciaDescripcion;
            this.CorreoSecretario = CorreoSecretario;
            this.CorreoJuez = CorreoJuez;
            this.CorreoEscribiente = CorreoEscribiente;
            this.FechaEmision = FechaEmision;
            this.AlertaInternacional = AlertaInternacional;
            this.DepartamentoId = DepartamentoId;
            this.DepartamentoDescripcion = DepartamentoDescripcion;
            this.MunicipioId = MunicipioId;
            this.MunicipioDescripcion = MunicipioDescripcion;
        }

        public void ApplyEvent(OrdenCapturaModificadaEvent @event)
        {
            this.OrdenCapturaId = OrdenCapturaId;
            this.OrganoJurisdiccionalId = OrganoJurisdiccionalId;
            this.OrganoJurisdiccionalDescripcion = OrganoJurisdiccionalDescripcion;
            this.OrdenCapturaCodigo = OrdenCapturaCodigo;
            this.NumeroOrdenCaptura = NumeroOrdenCaptura;
            this.Correlativo = Correlativo;
            this.OrdenCapturaEstadoId = OrdenCapturaEstadoId;
            this.OrdenCapturaEstadoDescripcion = OrdenCapturaEstadoDescripcion;
            this.ExpedienteId = ExpedienteId;
            this.NumeroExpediente = NumeroExpediente;
            this.InstanciaId = InstanciaId;
            this.InstanciaDescripcion = InstanciaDescripcion;
            this.CorreoSecretario = CorreoSecretario;
            this.CorreoJuez = CorreoJuez;
            this.CorreoEscribiente = CorreoEscribiente;
            this.FechaEmision = FechaEmision;
            this.FechaEntrega = FechaEntrega;
            this.AlertaInternacional = AlertaInternacional;
            this.DepartamentoId = DepartamentoId;
            this.DepartamentoDescripcion = DepartamentoDescripcion;
            this.MunicipioId = MunicipioId;
            this.MunicipioDescripcion = MunicipioDescripcion;
            this.Observaciones = Observaciones;
        }
    }
}
