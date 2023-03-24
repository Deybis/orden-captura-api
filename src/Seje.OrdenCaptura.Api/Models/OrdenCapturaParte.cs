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
        public string ParteDescripcion { get; set; }
    }
}
