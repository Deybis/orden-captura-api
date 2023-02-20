using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class TipoFirma
    {
        public int TipoFirmaId { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
