using System;
using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class Delito
    {
        public long Id { get; set; }
        public int DelitoId { get; set; }
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public long ExpedienteId { get; set; }
    }

    public class RegistrarDelito
    {
        [Required]
        public int DelitoId { get; set; }
        public Guid Codigo { get; set; }
        [Required]
        public string Nombre { get; set; }
        public long ExpedienteId { get; set; }
    }

    public class ActualizarDelito
    {
        public long Id { get; set; }
        public int DelitoId { get; set; }
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public long ExpedienteId { get; set; }
    }
}
