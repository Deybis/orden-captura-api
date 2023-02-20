using Entities.Shared.Audit;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seje.OrdenCaptura.QueryStack
{
    [Table("Partes")]
    public class Parte : Auditable, IAuditable
    {
        [Key]
        [DatabaseGenerat‌ed(DatabaseGeneratedOp‌​tion.Identity)]
        public int ParteId { get; set; }
        [Required]
        public int TipoParteId { get; set; }
        [Required]
        public string TipoParteDescripcion { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string NumeroIdentificacion{ get; set; }
        public string Domicilio { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public int SexoId { get; set; }
        public string SexoDescripcion { get; set; }
        public int PaisId { get; set; }
        public string PaisDescripcion { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string TipoIdentificacionDescripcion { get; set; }

        [Required]
        public int TipoPersonaId { get; set; }

        [Required]
        [MaxLength(250)]
        public string TipoPersonaDescripcion { get; set; }
        public Expediente Expediente { get; set; }
    }
}
