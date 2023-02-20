using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("TipoFirmas")]
    public class TipoFirma
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int TipoFirmaId { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
