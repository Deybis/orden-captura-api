using System;

namespace Seje.OrdenCaptura.Api
{
    public class FiltrosOrdenCaptura
    {
        public long OrdenCapturaId { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public Guid OrdenCapturaCodigo { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public int OrdenCapturaEstadoId { get; set; }
        public long ExpedienteId { get; set; }
        public string NumeroExpediente { get; set; }
        public int InstanciaId { get; set; }
        public string CorreoSecretario { get; set; }
        public string CorreoJuez { get; set; }
        public string CorreoEscribiente { get; set; }
        public bool AlertaInternacional { get; set; }
        public string FechaEmision { get; set; }
        public string FechaEntrega { get; set; }
        public string añoActual { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class FiltrosExpediente
    {
        public long Id { get; set; }
        public string NumeroExpediente { get; set; }
        public int OrganoJurisdiccionalId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class FiltrosDelito
    {
        public long Id { get; set; }
        public int DelitoId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public long ExpedienteId { get; set; }
    }

    public class FiltrosParte
    {
        public int ParteId { get; set; }
        public int TipoParteId { get; set; }
        public string Nombre { get; set; }
        public string NumeroIdentidad { get; set; }
        public int PersonaId { get; set; }
        public int SexoId { get; set; }
        public string SexoDescripcion { get; set; }
        public int TipoPersonaId { get; set; }
    }

    public class FiltrosOrdenCapturaEstado
    {
        public int OrdenCapturaEstadoId { get; set; }
        public string Descripcion { get; set; }
    }

    public class FiltrosFirma
    {
        public int FirmaId { get; set; }
        public long OrdenCapturaId { get; set; }
        public int TipoFirmaId { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public string CorreoFirmante { get; set; }
    }
    public class FiltrosConfiguracion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
    }

    public class FiltrosTipoFirma
    {
        public int TipoFirmaId { get; set; }
        public string Nombre { get; set; }
    }

    public class FiltrosTipoDocumento
    {
        public int TipoDocumentoId { get; set; }
        public string Nombre { get; set; }
    }

    public class FiltrosDocumento
    {
        public long DocumentoId { get; set; }
        public long OrdenCapturaId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumeroOrdenCaptura { get; set; }
        public Guid Codigo { get; set; }
        public string Nombre { get; set; }
        public string TipoMedia { get; set; }
        public string Descripcion { get; set; }
        public long Peso { get; set; }
        public string Extension { get; set; }
        public string Ubicacion { get; set; }
        public string Origen { get; set; }
        public bool Finalizado { get; set; }
    }

    public class FiltrosFirmante
    {
        public int FirmanteId { get; set; }
        public string Identificador { get; set; }
    }
}
