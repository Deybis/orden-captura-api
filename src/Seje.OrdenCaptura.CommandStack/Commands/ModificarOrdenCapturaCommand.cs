﻿using Seje.OrdenCaptura.SharedKernel;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;

namespace Seje.OrdenCaptura.CommandStack.Commands
{
    public class ModificarOrdenCapturaCommand : SejeCommand<IResult<Models.OrdenCaptura>>
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

        public ModificarOrdenCapturaCommand(
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
            int DepartamentoId,
            string DepartamentoDescripcion,
            int MunicipioId,
            string MunicipioDescripcion,
            string Observaciones,
            string UserName
        ) : base(UserName)
        {
            this.OrdenCapturaId = OrdenCapturaId;
            this.OrganoJurisdiccionalId = OrganoJurisdiccionalId;
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
