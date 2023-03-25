using System;
using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class OrdenCapturaParte
    {
        public long Id { get; set; }

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
    }
}
