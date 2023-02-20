using Entities.Shared.Audit;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("Documentos")]
    public class Documento : Auditable, IAuditable
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public long DocumentoId { get; set; }

        [Required]
        public long OrdenCapturaId { get; set; }

        [Required]
        public int TipoDocumentoId { get; set; }

        [Required]
        public string NumeroOrdenCaptura { get; set; }

        [Required]
        public Guid Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string TipoMedia { get; set; }
        public string Descripcion { get; set; }
        public long Peso { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }

        [Required]
        public bool Finalizado { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public OrdenCaptura OrdenCaptura { get; set; }
    }
}
