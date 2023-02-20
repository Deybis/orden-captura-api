using System;
using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class Parte
    {
        public int ParteId { get; set; }
        [Required]
        public int TipoParteId { get; set; }
        public string TipoParteDescripcion { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string NumeroIdentidad { get; set; }

        [Required]
        public string Domicilio { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string NumeroIdentificacion { get; set; }
        public int PaisId { get; set; }
        public string PaisDescripcion { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string TipoIdentificacionDescripcion { get; set; }

        [Required]
        public int SexoId { get; set; }
        public string SexoDescripcion { get; set; }

        [Required]
        public int TipoPersonaId { get; set; }
        public string TipoPersonaDescripcion { get; set; }
    }

    public class RegistrarParte
    {
        [Required]
        public int TipoParteId { get; set; }
        public string TipoParteDescripcion { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string NumeroIdentidad { get; set; }

        [Required]
        public string Domicilio { get; set; }

        public string FechaNacimiento { get; set; }
        public string NumeroIdentificacion { get; set; }
        public int PaisId { get; set; }
        public string PaisDescripcion { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string TipoIdentificacionDescripcion { get; set; }

        [Required]
        public int SexoId { get; set; }
        public string SexoDescripcion { get; set; }

        [Required]
        public int TipoPersonaId { get; set; }
        public string TipoPersonaDescripcion { get; set; }
    }

    public class ActualizarParte
    {
        public int ParteId { get; set; }
        [Required]
        public int TipoParteId { get; set; }
        public string TipoParteDescripcion { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string NumeroIdentidad { get; set; }

        [Required]
        public string Domicilio { get; set; }

        public string FechaNacimiento { get; set; }
        public string NumeroIdentificacion { get; set; }
        public int PaisId { get; set; }
        public string PaisDescripcion { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string TipoIdentificacionDescripcion { get; set; }

        [Required]
        public int SexoId { get; set; }
        public string SexoDescripcion { get; set; }

        [Required]
        public int TipoPersonaId { get; set; }
        public string TipoPersonaDescripcion { get; set; }
    }
}
