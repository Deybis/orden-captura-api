using System.Linq;
namespace Seje.OrdenCaptura.QueryStack
{
    public interface IDatabase
    {
        IQueryable<OrdenCaptura> OrdenesCaptura { get; }
        IQueryable<Expediente> Expedientes { get; }
        IQueryable<Parte> Partes { get; }
        IQueryable<Delito> Delitos { get; }
        IQueryable<OrdenCapturaEstado> OrdenCapturaEstados { get; }
        IQueryable<OrdenCapturaParte> OrdenCapturaPartes { get; }
        IQueryable<TipoFirma> TipoFirmas { get; }
        IQueryable<Firma> Firmas { get; }
        IQueryable<Configuracion> Configuraciones { get; }
        IQueryable<Documento> Documentos { get; }
        IQueryable<Firmante> Firmantes { get; }
        IQueryable<TipoDocumento> TipoDocumentos { get; }
    }
}
