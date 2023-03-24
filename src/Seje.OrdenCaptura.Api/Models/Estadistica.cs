using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Models
{
    public class Estadistica
    {
        public int? TotalBorrador { get; set; }
        public int? TotalRechazadas { get; set; }
        public int? TotalEnRevision { get; set; }
        public int? TotalPendienteDeFirma { get; set; }
        public int? TotalPendienteDeEntrega { get; set; }
        public int? TotalActivas { get; set; }
        public int? TotalContraCapturaPendienteDeFirma { get; set; }
        public int? TotalContraCaptura { get; set; }
        public int? TotalEjecutadas { get; set; }
        public int? Total { get; set; }
    }
}
