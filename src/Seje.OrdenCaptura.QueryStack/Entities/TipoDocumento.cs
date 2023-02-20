using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("TipoDocumentos")]
    public class TipoDocumento
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int TipoDocumentoId { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
