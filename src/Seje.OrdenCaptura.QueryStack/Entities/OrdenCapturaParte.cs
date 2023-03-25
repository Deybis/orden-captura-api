using Entities.Shared.Audit;
using System;
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
        public int TipoParteId { get; set; }
        public string TipoParteDescripcion { get; set; }
        public string Nombre { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string Domicilio { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public OrdenCaptura OrdenCaptura { get; set; }
    }
}
