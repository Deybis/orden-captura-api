using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Shared.Audit
{
    public class Auditable : IAuditable
    {
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [MaxLength(254)]
        public string UsuarioCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
        [Required]
        [MaxLength(254)]
        public string UsuarioModificacion { get; set; }
    }
}
