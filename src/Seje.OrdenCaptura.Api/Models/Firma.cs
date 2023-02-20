using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class Firma
    {
        public int FirmaId { get; set; }

        [Required]
        public string NumeroOrdenCaptura { get; set; }

        [Required]
        public string CorreoFirmante { get; set; }

        [Required]
        public bool Firmo { get; set; }

        [Required]
        public int TipoFirmaId { get; set; }

        [Required]
        public long OrdenCapturaId { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion{ get; set; }

        public TipoFirma TipoFirma { get; set; }
    }

    public class AgregarFirmas
    {
        public string NumeroOrdenCaptura { get; set; }
        public int NumeroFirmas { get; set; }
    }

}
