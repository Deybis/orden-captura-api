using System;
using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class OrdenCaptura
    {
        public long OrdenCapturaId { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public Guid OrdenCapturaCodigo { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public int Correlativo { get; set; }
        public int OrdenCapturaEstadoId { get; set; }
        public string OrdenCapturaEstadoDescripcion { get; set; }
        public int ExpedienteId { get; set; }
        public string NumeroExpediente { get; set; }
        public int InstanciaId { get; set; }
        public string InstanciaDescripcion { get; set; }
        public string CorreoSecretario { get; set; }
        public string CorreoJuez { get; set; }
        public string CorreoEscribiente { get; set; }
        public bool AlertaInternacional { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEntrega { get; set; }
    }

    public class RegistrarOrdenCaptura
    {
        [Required]
        public int OrganoJurisdiccionalId { get; set; }

        [Required]
        public string NumeroOrdenCaptura { get; set; }

        [Required]
        public int Correlativo { get; set; }

        public int ExpedienteId { get; set; }

        [Required]
        public string NumeroExpediente { get; set; }

        [Required]
        public int InstanciaId { get; set; }

        [MaxLength(250)]
        public string InstanciaDescripcion { get; set; }

        [Required]
        public string CorreoSecretario { get; set; }

        [Required]
        public string CorreoJuez { get; set; }

        [Required]
        public string CorreoEscribiente { get; set; }

        [Required]
        public string FechaEmision { get; set; }
        public string FechaEntrega { get; set; }

        [Required]
        public bool AlertaInternacional { get; set; }
    }
    public class ActualizarOrdenCaptura
    {
        public long OrdenCapturaId { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public Guid OrdenCapturaCodigo { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public int Correlativo { get; set; }
        public int OrdenCapturaEstadoId { get; set; }
        public string OrdenCapturaEstadoDescripcion { get; set; }
        public int ExpedienteId { get; set; }
        public string NumeroExpediente { get; set; }
        public int InstanciaId { get; set; }
        public string InstanciaDescripcion { get; set; }
        public string CorreoSecretario { get; set; }
        public string CorreoJuez { get; set; }
        public string CorreoEscribiente { get; set; }
        public string FechaEmision { get; set; }
        public string FechaEntrega { get; set; }
        public bool AlertaInternacional { get; set; }
    }

    public class OrdenCapturaPDF
    {
        public string Institucion { get; set; }
        public string PdfBase64 { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }

}
