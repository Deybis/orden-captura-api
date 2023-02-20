using System.Collections.Generic;

namespace Entities.Shared.Model
{
    public class OrdenCapturaFormato
    {
        public string NumeroExpediente { get; set; }
        public string Numerojuez { get; set; }
        public string NumeroFormato { get; set; }
        public string Juzgado { get; set; }
        public string Ciudad { get; set; }
        public string FechaEmision { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public long OrdenCapturaId { get; set; }
        public List<Institucion> Instituciones { get; set; }
        public ExpedienteDetalle ExpedienteDetalle { get; set; }
        public Funcionario Juez { get; set; }
        public Funcionario Secretario { get; set; }
    }
    
}
