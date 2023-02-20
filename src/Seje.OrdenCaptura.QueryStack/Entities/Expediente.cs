using Entities.Shared.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("Expedientes")]
    public class Expediente : Auditable, IAuditable
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public long Id { get; set; }
        [Required]
        public string NumeroExpediente { get; set; }
        [Required]
        public int OrganoJurisdiccionalId { get; set; }
        public string OrganoJurisdiccionalDescripcion { get; set; }
        public int ProcedenciaId { get; set; }
        public string ProcedenciaDescripcion { get; set; }
        public int ProcesoId { get; set; }
        public string ProcesoDescripcion { get; set; }

        [Required]
        public int MateriaId { get; set; }
        [MaxLength(250)]
        public string MateriaDescripcion { get; set; }
        [Required]
        public DateTime FechaRecepcion { get; set; }
        public virtual List<Parte> Partes { get; set; } = new List<Parte>();
        public virtual List<Delito> Delitos { get; set; } = new List<Delito>();
    }
}
