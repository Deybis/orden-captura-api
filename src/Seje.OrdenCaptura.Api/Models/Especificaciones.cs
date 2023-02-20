using Ardalis.Specification;
using System;
using System.Linq;

namespace Seje.OrdenCaptura.Api.Models
{
    public class OrdenCapturaSpec : Specification<QueryStack.OrdenCaptura>, ISingleResultSpecification
    {
        public OrdenCapturaSpec(FiltrosOrdenCaptura filtro)
        {
            if (filtro.OrdenCapturaId > 0)
                Query.Where(x => x.OrdenCapturaId == filtro.OrdenCapturaId);

            if (!string.IsNullOrWhiteSpace(filtro.NumeroExpediente))
                Query.Where(x => x.NumeroExpediente == filtro.NumeroExpediente);

            if (!string.IsNullOrWhiteSpace(filtro.NumeroOrdenCaptura))
                Query.Where(x => x.NumeroOrdenCaptura == filtro.NumeroOrdenCaptura);

            if (filtro.OrganoJurisdiccionalId > 0)
                Query.Where(x => x.OrganoJurisdiccionalId == filtro.OrganoJurisdiccionalId);

            if (!string.IsNullOrWhiteSpace(filtro.añoActual))
                Query.Where(x => x.FechaEmision.Year == Convert.ToInt32(filtro.añoActual));

            if (!string.IsNullOrWhiteSpace(filtro.CorreoEscribiente) || !string.IsNullOrWhiteSpace(filtro.CorreoEscribiente) || !string.IsNullOrWhiteSpace(filtro.CorreoEscribiente))
                Query.Where(x => x.CorreoEscribiente == filtro.CorreoEscribiente || x.CorreoJuez == filtro.CorreoJuez || x.CorreoSecretario == filtro.CorreoSecretario);
        }
    }

    public class OrdenCapturaPaginationSpec : Specification<QueryStack.OrdenCaptura>, ISingleResultSpecification
    {
        public OrdenCapturaPaginationSpec(FiltrosOrdenCaptura filtro)
        {
            if (filtro.OrdenCapturaId > 0)
                Query.Where(x => x.OrdenCapturaId == filtro.OrdenCapturaId);

            if (filtro.OrganoJurisdiccionalId > 0)
                Query.Where(x => x.OrganoJurisdiccionalId == filtro.OrganoJurisdiccionalId);

            if (!string.IsNullOrWhiteSpace(filtro.NumeroOrdenCaptura))
                Query.Where(x => x.NumeroOrdenCaptura == filtro.NumeroOrdenCaptura);

            if (!string.IsNullOrWhiteSpace(filtro.CorreoEscribiente) || !string.IsNullOrWhiteSpace(filtro.CorreoEscribiente) || !string.IsNullOrWhiteSpace(filtro.CorreoEscribiente))
                Query.Where(x => x.CorreoEscribiente == filtro.CorreoEscribiente || x.CorreoJuez == filtro.CorreoJuez || x.CorreoSecretario == filtro.CorreoSecretario);

            var skip = (filtro.PageNumber - 1) * filtro.PageSize;
            var take = filtro.PageSize;

            Query.Skip(skip).Take(take);
        }
    }

    public class ExpedientePaginationSpec : Specification<QueryStack.Expediente>, ISingleResultSpecification
    {
        public ExpedientePaginationSpec(FiltrosExpediente filtro)
        {
            if (filtro.Id > 0)
                Query.Where(x => x.Id == filtro.Id);

            if(!string.IsNullOrWhiteSpace(filtro.NumeroExpediente))
                Query.Where(x => x.NumeroExpediente == filtro.NumeroExpediente);

            if (filtro.OrganoJurisdiccionalId > 0)
                Query.Where(x => x.OrganoJurisdiccionalId == filtro.OrganoJurisdiccionalId);

            var skip = (filtro.PageNumber - 1) * filtro.PageSize;
            var take = filtro.PageSize;

            Query.Skip(skip).Take(take);
            Query.Include(e => e.Partes);
            Query.Include(e => e.Delitos);
        }
    }

    public class ExpedienteSpec : Specification<QueryStack.Expediente>, ISingleResultSpecification
    {
        public ExpedienteSpec(FiltrosExpediente filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro.NumeroExpediente))
                Query.Where(x => x.NumeroExpediente == filtro.NumeroExpediente);

            Query.Include(e => e.Partes);
            Query.Include(e => e.Delitos);
        }
    }

    public class DelitoSpec : Specification<QueryStack.Delito>, ISingleResultSpecification
    {
        public DelitoSpec(FiltrosDelito filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                Query.Where(x => x.Nombre == filtro.Nombre);
        }
    }

    public class ParteSpec : Specification<QueryStack.Parte>, ISingleResultSpecification
    {
        public ParteSpec(FiltrosParte filtro)
        {
            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                Query.Where(x => x.Nombre == filtro.Nombre);

            if (filtro.TipoParteId > 0)
                Query.Where(x => x.TipoParteId == filtro.TipoParteId);
        }
    }

    public class OrdenCapturaEstadoSpec : Specification<QueryStack.OrdenCapturaEstado>, ISingleResultSpecification
    {
        public OrdenCapturaEstadoSpec(FiltrosOrdenCapturaEstado filtro)
        {
            if (filtro.OrdenCapturaEstadoId > 0)
                Query.Where(x => x.OrdenCapturaEstadoId == filtro.OrdenCapturaEstadoId);

            if (!string.IsNullOrWhiteSpace(filtro.Descripcion))
                Query.Where(x => x.Descripcion == filtro.Descripcion);
        }
    }

    public class FirmaSpec : Specification<QueryStack.Firma>, ISingleResultSpecification
    {
        public FirmaSpec(FiltrosFirma filtro)
        {
            Query.Include(x => x.TipoFirma);

            if (filtro.FirmaId > 0)
                Query.Where(x => x.FirmaId == filtro.FirmaId);

            if (filtro.OrdenCapturaId > 0)
                Query.Where(x => x.OrdenCapturaId== filtro.OrdenCapturaId);

            if (filtro.TipoFirmaId > 0)
                Query.Where(x => x.TipoFirmaId == filtro.TipoFirmaId);

            if (!string.IsNullOrWhiteSpace(filtro.CorreoFirmante))
                Query.Where(x => x.CorreoFirmante == filtro.CorreoFirmante);

            if (!string.IsNullOrWhiteSpace(filtro.NumeroOrdenCaptura))
                Query.Where(x => x.NumeroOrdenCaptura == filtro.NumeroOrdenCaptura);
        }
    }

    public class ConfiguracionSpec : Specification<QueryStack.Configuracion>, ISingleResultSpecification
    {
        public ConfiguracionSpec(FiltrosConfiguracion filtro)
        {
            if (filtro.Id > 0)
                Query.Where(x => x.Id == filtro.Id);

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                Query.Where(x => x.Nombre == filtro.Nombre);

            if (!string.IsNullOrWhiteSpace(filtro.Valor))
                Query.Where(x => x.Valor == filtro.Valor);
        }
    }

    public class TipoFirmaSpec : Specification<QueryStack.TipoFirma>, ISingleResultSpecification
    {
        public TipoFirmaSpec(FiltrosTipoFirma filtro)
        {
            if (filtro.TipoFirmaId > 0)
                Query.Where(x => x.TipoFirmaId == filtro.TipoFirmaId);

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                Query.Where(x => x.Nombre == filtro.Nombre);
        }
    }

    public class DocumentoSpec : Specification<QueryStack.Documento>, ISingleResultSpecification
    {
        public DocumentoSpec(FiltrosDocumento filtro)
        {
            if (filtro.DocumentoId > 0)
                Query.Where(x => x.DocumentoId == filtro.DocumentoId);

            if (filtro.OrdenCapturaId > 0)
                Query.Where(x => x.OrdenCapturaId == filtro.DocumentoId);

            if (filtro.TipoDocumentoId > 0)
                Query.Where(x => x.TipoDocumentoId == filtro.TipoDocumentoId);

            if (!string.IsNullOrWhiteSpace(filtro.NumeroOrdenCaptura))
                Query.Where(x => x.NumeroOrdenCaptura == filtro.NumeroOrdenCaptura);

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                Query.Where(x => x.Nombre == filtro.Nombre);

            if (filtro.Codigo != Guid.Empty)
                Query.Where(x => x.Codigo == filtro.Codigo);

            if (filtro.Finalizado)
                Query.Where(x => x.Finalizado == filtro.Finalizado);
        }
    }

    public class TipoDocumentoSpec : Specification<QueryStack.TipoDocumento>, ISingleResultSpecification
    {
        public TipoDocumentoSpec(FiltrosTipoDocumento filtro)
        {
            if (filtro.TipoDocumentoId > 0)
                Query.Where(x => x.TipoDocumentoId == filtro.TipoDocumentoId);

            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
                Query.Where(x => x.Nombre == filtro.Nombre);
        }
    }

    public class FirmanteSpec : Specification<QueryStack.Firmante>, ISingleResultSpecification
    {
        public FirmanteSpec(FiltrosFirmante filtro)
        {
            if (filtro.FirmanteId > 0)
                Query.Where(x => x.FirmanteId == filtro.FirmanteId);

            if (!string.IsNullOrWhiteSpace(filtro.Identificador))
                Query.Where(x => x.Identificador == filtro.Identificador);
        }
    }
}
