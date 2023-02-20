using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class Configuracion
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Valor { get; set; }
    }
}
