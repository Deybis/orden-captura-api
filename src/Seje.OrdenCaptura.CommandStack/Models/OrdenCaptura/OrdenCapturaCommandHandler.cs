using Seje.OrdenCaptura.CommandStack.Events;
using System;

namespace Seje.OrdenCaptura.CommandStack.Models
{
    public partial class OrdenCaptura
    {
        public void RegistrarOrdenCaptura(
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
            var @event = new OrdenCapturaRegistradaEvent(
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
            RaiseEvent(@event);
        }

        public void ModificarOrdenCaptura(
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
            var @event = new OrdenCapturaModificadaEvent(
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
            RaiseEvent(@event);
        }
    }
}
