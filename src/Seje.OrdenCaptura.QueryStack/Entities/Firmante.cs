using Entities.Shared.Audit;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("Firmantes")]
    [Index(nameof(Identificador), IsUnique = true)]
    public class Firmante
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int FirmanteId { get; set; }

        [Required]
        public string Identificador { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal CoordX { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal CoordY { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Ancho { get; set; }

        [Required]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Alto { get; set; }
    }
}
