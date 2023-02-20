using Microsoft.EntityFrameworkCore;

namespace Seje.OrdenCaptura.QueryStack
{
    public class OrdenCapturaDbContext : DbContext
    {
        public OrdenCapturaDbContext(DbContextOptions<OrdenCapturaDbContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<OrdenCaptura> OrdenesCaptura { get; set; }
        public virtual DbSet<Expediente> Expedientes { get; set; }
        public virtual DbSet<Parte> Partes { get; set; }
        public virtual DbSet<Delito> Delitos { get; set; }
        public virtual DbSet<OrdenCapturaEstado> OrdenCapturaEstados { get; set; }
        public virtual DbSet<TipoFirma> TipoFirmas { get; set; }
        public virtual DbSet<Firma> Firmas { get; set; }
        public virtual DbSet<Configuracion> Configuraciones { get; set; }
        public virtual DbSet<Documento> Documentos { get; set; }
        public virtual DbSet<Firmante> Firmantes { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }
    }
}
