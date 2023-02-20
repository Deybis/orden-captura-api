using Entities.Shared.Audit;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("Delitos")]
    public class Delito : Auditable, IAuditable
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public long Id { get; set; }

        [Required]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.None)]
        public int DelitoId { get; set; }

        [Required]
        public Guid Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public long ExpedienteId { get; set; }
        public Expediente Expediente { get; set; }
    }
}
