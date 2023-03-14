using System;
using System.Collections.Generic;

namespace Seje.Expediente.Client.Models
{
    public partial class Expediente
    {
        public string NumeroExpediente { get; set; }
        public string Juzgado { get; set; }
        public object Abogado { get; set; }
        public long NumeroJuez { get; set; }
        public object Materia { get; set; }
        public object Departamento { get; set; }
        public object Municipio { get; set; }
        public long JuzgadoId { get; set; }
        public object[] Prtes { get; set; }
        public Delito[] Delitos { get; set; }
        public Parte[] Partes { get; set; }
        public DateTimeOffset FechaHora { get; set; }
        public long UltimoFolio { get; set; }
    }

    public partial class Delito
    {
        public long DelitoId { get; set; }
        public Guid AlternateKey { get; set; }
        public long ExpedienteId { get; set; }
        public string Descripcion { get; set; }
        public DateTimeOffset FechaCreacion { get; set; }
        public object UsuarioCreacion { get; set; }
        public DateTimeOffset FechaModificacion { get; set; }
        public object UsuarioModificacion { get; set; }
        public Guid UsuarioId { get; set; }
        public bool Estado { get; set; }
        public long OrganoJurisdiccionalIdRemitido { get; set; }
        public object OrganoJurisdiccionalRemitido { get; set; }
    }

    public partial class Parte
    {
        public string Nombre { get; set; }
        public string TipoParte { get; set; }
        public string Identidad { get; set; }
        public string NombreTipoParte { get; set; }
    }
}
