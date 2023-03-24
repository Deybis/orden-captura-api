using Entities.Shared.Audit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("OrdenCapturaPartes")]
    public class OrdenCapturaParte : Auditable, IAuditable
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int Id { get; set; }

        [Required]
        public long OrdenCapturaId { get; set; }

        [Required]
        public string NumeroOrdenCaptura { get; set; }

        [Required]
        public int ParteId { get; set; }
        public string ParteDescripcion { get; set; }
        public OrdenCaptura OrdenCaptura { get; set; }
    }
}
