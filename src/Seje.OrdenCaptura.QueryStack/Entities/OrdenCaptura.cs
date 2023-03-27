using Entities.Shared.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("OrdenesCaptura")]
    public class OrdenCaptura : Auditable, IAuditable
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public long OrdenCapturaId { get; set; }

        [Required]
        public int OrganoJurisdiccionalId { get; set; }

        public string OrganoJurisdiccionalDescripcion { get; set; }

        [Required]
        public Guid OrdenCapturaCodigo { get; set; }

        [Required]
        public string NumeroOrdenCaptura { get; set; }

        [Required]
        public int Correlativo { get; set; }

        [Required]
        public int OrdenCapturaEstadoId { get; set; }
        public string OrdenCapturaEstadoDescripcion { get; set; }

        public long ExpedienteId { get; set; }

        [Required]
        public string NumeroExpediente { get; set; }

        [Required]
        public int InstanciaId { get; set; }

        public string InstanciaDescripcion { get; set; }

        [Required]
        public string CorreoSecretario { get; set; }

        [Required]
        public string CorreoEscribiente { get; set; }

        [Required]
        public string CorreoJuez { get; set; }

        [Required]
        public bool AlertaInternacional { get; set; }

        [Required]
        public int DepartamentoId { get; set; }

        [Required]
        public string DepartamentoDescripcion { get; set; }

        [Required]
        public int MunicipioId { get; set; }

        [Required]
        public string MunicipioDescripcion { get; set; }

        public string Observaciones { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }
        public DateTime FechaEntrega { get; set; }
        public virtual OrdenCapturaEstado OrdenCapturaEstado { get; set; }
        public virtual OrdenCapturaParte OrdenCapturaParte { get; set; }
        public virtual List<Firma> Firmas { get; set; } = new List<Firma>();
        public virtual List<Documento> Documentos { get; set; } = new List<Documento>();
    }
}
