using System;
using System.Collections.Generic;

namespace Seje.OrdenCaptura.Api
{
    public class Expediente
    {
        public long Id { get; set; }
        public string NumeroExpediente { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public string OrganoJurisdiccionalDescripcion { get; set; }
        public int ProcedenciaId { get; set; }
        public string ProcedenciaDescripcion { get; set; }
        public int ProcesoId { get; set; }
        public string ProcesoDescripcion { get; set; }
        public int MateriaId { get; set; }
        public string MateriaDescripcion { get; set; }
        public string FechaRecepcion { get; set; }
        public List<Parte> Partes { get; set; } = new List<Parte>();
        public List<Delito> Delitos { get; set; } = new List<Delito>();
    }

    public class RegistrarExpediente
    {
        public string NumeroExpediente { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public string OrganoJurisdiccionalDescripcion { get; set; }
        public int ProcedenciaId { get; set; }
        public string ProcedenciaDescripcion { get; set; }
        public int ProcesoId { get; set; }
        public string ProcesoDescripcion { get; set; }
        public int MateriaId { get; set; }
        public string MateriaDescripcion { get; set; }
        public string FechaRecepcion { get; set; }
        public List<Parte> Partes { get; set; }
        public List<Delito> Delitos { get; set; }
    }

    public class ActualizarExpediente
    {
        public long Id { get; set; }
        public string NumeroExpediente { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public string OrganoJurisdiccionalDescripcion { get; set; }
        public int ProcedenciaId { get; set; }
        public string ProcedenciaDescripcion { get; set; }
        public int ProcesoId { get; set; }
        public string ProcesoDescripcion { get; set; }
        public int MateriaId { get; set; }
        public string MateriaDescripcion { get; set; }
        public string FechaRecepcion { get; set; }
        public List<Parte> Partes { get; set; }
        public List<Delito> Delitos { get; set; }
    }
}
