namespace Seje.OrdenCaptura.Api
{
    public enum OrdenCapturaEstados
    {
        Borrador = 1,
        EnRevision = 2,
        Rechazada = 3,
        PendienteDeFirma = 4,
        PendienteDeEntrega = 5,
        Activa = 6,
        ContraCapturaPendienteDeFirma = 7,
        ContraCaptura = 8,
        Ejecutada = 9
    }
    public enum TipoFirmas
    {
        OrdenCaptura = 1,
        ContraOrdenCaptura = 2
    }
    public enum TipoDocumento
    {
        OrdenCaptura = 1,
        ContraOrdenCaptura = 2,
        AcuseDeRecibo = 3,
        OficioOrdenDeCapturaEjecutada = 4
    }
}
