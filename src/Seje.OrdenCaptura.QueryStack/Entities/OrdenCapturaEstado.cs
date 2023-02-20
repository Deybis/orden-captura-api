using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("OrdenCapturaEstados")]
    public class OrdenCapturaEstado
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int OrdenCapturaEstadoId { get; set; }

        [MaxLength(250)]
        public string Descripcion { get; set; }
    }
}
