using System;

namespace Seje.OrdenCaptura.CommandStack.Models
{
    public partial class OrdenCaptura
    {
        public class Factory
        {
            public static OrdenCaptura Create(
                long OrdenCapturaId,
                int OrganoJurisdiccionalId,
                string OrganoJurisdiccionalDescripcion,
                Guid OrdenCapturaCodigo,
                string NumeroOrdenCaptura,
                int Correlativo,
                int OrdenCapturaEstadoId,
                string OrdenCapturaEstadoDescripcion,
                long ExpedienteId,
                string NumeroExpediente,
                int InstanciaId,
                string InstanciaDescripcion,
                string CorreoSecretario,
                string CorreoJuez,
                string CorreoEscribiente,
                DateTime FechaEmision,
                bool AlertaInternacional,
                int DepartamentoId,
                string DepartamentoDescripcion,
                int MunicipioId,
                string MunicipioDescripcion,
                string UserName)
            {
                var @event = new Events.OrdenCapturaRegistradaEvent(
                    OrdenCapturaId,
                    OrganoJurisdiccionalId,
                    OrganoJurisdiccionalDescripcion,
                    OrdenCapturaCodigo,
                    NumeroOrdenCaptura,
                    Correlativo,
                    OrdenCapturaEstadoId,
                    OrdenCapturaEstadoDescripcion,
                    ExpedienteId,
                    NumeroExpediente,
                    InstanciaId,
                    InstanciaDescripcion,
                    CorreoSecretario,
                    CorreoJuez,
                    CorreoEscribiente,
                    FechaEmision,
                    AlertaInternacional,
                    DepartamentoId,
                    DepartamentoDescripcion,
                    MunicipioId,
                    MunicipioDescripcion,
                    UserName);

                var aggregate = new OrdenCaptura();
                aggregate.RaiseEvent(@event);
                return aggregate;
            }

            public static OrdenCaptura Modify(
                long OrdenCapturaId,
                int OrganoJurisdiccionalId,
                string OrganoJurisdiccionalDescripcion,
                Guid OrdenCapturaCodigo,
                string NumeroOrdenCaptura,
                int Correlativo,
                int OrdenCapturaEstadoId,
                string OrdenCapturaEstadoDescripcion,
                long ExpedienteId,
                string NumeroExpediente,
                int InstanciaId,
                string InstanciaDescripcion,
                string CorreoSecretario,
                string CorreoJuez,
                string CorreoEscribiente,
                DateTime FechaEmision,
                DateTime FechaEntrega,
                bool AlertaInternacional,
                int DepartamentoId,
                string DepartamentoDescripcion,
                int MunicipioId,
                string MunicipioDescripcion,
                string Observaciones,
                string UserName)
            {
                var @event = new Events.OrdenCapturaModificadaEvent(
                    OrdenCapturaId,
                    OrganoJurisdiccionalId,
                    OrganoJurisdiccionalDescripcion,
                    OrdenCapturaCodigo,
                    NumeroOrdenCaptura,
                    Correlativo,
                    OrdenCapturaEstadoId,
                    OrdenCapturaEstadoDescripcion,
                    ExpedienteId,
                    NumeroExpediente,
                    InstanciaId,
                    InstanciaDescripcion,
                    CorreoSecretario,
                    CorreoJuez,
                    CorreoEscribiente,
                    FechaEmision,
                    FechaEntrega,
                    AlertaInternacional,
                    DepartamentoId,
                    DepartamentoDescripcion,
                    MunicipioId,
                    MunicipioDescripcion,
                    Observaciones,
                    UserName);

                var aggregate = new OrdenCaptura();
                aggregate.RaiseEvent(@event);
                return aggregate;
            }
        }
    }
}
