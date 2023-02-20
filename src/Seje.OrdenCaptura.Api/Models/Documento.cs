using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Seje.OrdenCaptura.Api
{
    public class Documento
    {
        public long DocumentoId { get; set; }

        [Required]
        public long OrdenCapturaId { get; set; }

        [Required]
        public int TipoDocumentoId { get; set; }

        [Required]
        public string NumeroOrdenCaptura { get; set; }

        [Required]
        public Guid Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string TipoMedia { get; set; }
        public string Descripcion { get; set; }
        public long Peso { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }
        public string Base64String { get; set; }
        public string Url { get; set; }
        public bool Finalizado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public OrdenCaptura OrdenCaptura { get; set; }
    }

    public class RegistrarDocumento
    {
        public string Base64String { get; set; }
        public long OrdenCapturaId { get; set; }
        public int OrdenCapturaEstadoId { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public int TipoDocumentoId { get; set; }
        public string FilePath { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public int Size { get; set; }
        public string Extension { get; set; }
    }

}
