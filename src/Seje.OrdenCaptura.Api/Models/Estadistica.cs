using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Models
{
    public class Estadistica
    {
        public int? TotalOrdenesCaptura { get; set; }
        public List<EstadisticaEstado> Estados { get; set; }
    }

    public class EstadisticaEstado
    {
        public string Estado { get; set; }
        public int Total { get; set; }
    }
}
