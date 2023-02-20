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
                string UserName)
            {
                var @event = new Events.OrdenCapturaRegistradaEvent(
                    OrdenCapturaId,
                    OrganoJurisdiccionalId,
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
                    UserName);

                var aggregate = new OrdenCaptura();
                aggregate.RaiseEvent(@event);
                return aggregate;
            }

            public static OrdenCaptura Modify(
                long OrdenCapturaId,
                int OrganoJurisdiccionalId,
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
                string UserName)
            {
                var @event = new Events.OrdenCapturaModificadaEvent(
                    OrdenCapturaId,
                    OrganoJurisdiccionalId,
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
                    UserName);

                var aggregate = new OrdenCaptura();
                aggregate.RaiseEvent(@event);
                return aggregate;
            }
        }
    }
}
