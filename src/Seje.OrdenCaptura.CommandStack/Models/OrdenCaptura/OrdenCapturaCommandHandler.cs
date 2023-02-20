using Seje.OrdenCaptura.CommandStack.Events;
using System;

namespace Seje.OrdenCaptura.CommandStack.Models
{
    public partial class OrdenCaptura
    {
        public void RegistrarOrdenCaptura(
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
            var @event = new OrdenCapturaRegistradaEvent(
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
            RaiseEvent(@event);
        }

        public void ModificarOrdenCaptura(
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
            var @event = new OrdenCapturaModificadaEvent(
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
            RaiseEvent(@event);
        }
    }
}
