using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Seje.OrdenCaptura.QueryStack
{
    public class Database : IDatabase
    {
        private readonly OrdenCapturaDbContext context;

        public Database(OrdenCapturaDbContext context)
        {
            this.context = context;
        }

        public IQueryable<OrdenCaptura> OrdenesCaptura => context.OrdenesCaptura.AsNoTracking();
        public IQueryable<Expediente> Expedientes => context.Expedientes.AsNoTracking();
        public IQueryable<Parte> Partes => context.Partes.AsNoTracking();
        public IQueryable<Delito> Delitos => context.Delitos.AsNoTracking();
        public IQueryable<OrdenCapturaEstado> OrdenCapturaEstados => context.OrdenCapturaEstados.AsNoTracking();
        public IQueryable<OrdenCapturaParte> OrdenCapturaPartes => context.OrdenCapturaPartes.AsNoTracking();
        public IQueryable<TipoFirma> TipoFirmas => context.TipoFirmas.AsNoTracking();
        public IQueryable<Firma> Firmas => context.Firmas.AsNoTracking();
        public IQueryable<Configuracion> Configuraciones => context.Configuraciones.AsNoTracking();
        public IQueryable<Documento> Documentos => context.Documentos.AsNoTracking();
        public IQueryable<Firmante> Firmantes => context.Firmantes.AsNoTracking();
        public IQueryable<TipoDocumento> TipoDocumentos => context.TipoDocumentos.AsNoTracking();
    }
}
