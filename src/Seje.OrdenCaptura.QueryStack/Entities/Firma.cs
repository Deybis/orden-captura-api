using Entities.Shared.Audit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("Firmas")]
    public class Firma : Auditable, IAuditable
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int FirmaId { get; set; }

        [Required]
        public int TipoFirmaId { get; set; }

        [Required]
        public long OrdenCapturaId { get; set; }

        [Required]
        public string NumeroOrdenCaptura { get; set; }

        [Required]
        public string CorreoFirmante { get; set; }

        [Required]
        public bool Firmo { get; set; }
        public TipoFirma TipoFirma { get; set; }
        public OrdenCaptura OrdenCaptura { get; set; }
    }
}
