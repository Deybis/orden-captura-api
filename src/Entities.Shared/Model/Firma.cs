namespace Entities.Shared.Model
{
    public class FirmaRequest
    {
        public string File { get; set; }
        public string FirmaMode { get; set; }
        public string UserName { get; set; }
        public string SignHeight { get; set; }
        public string SignWidth { get; set; }
        public string SignX { get; set; }
        public string SignY { get; set; }
        public int TipoDocumento { get; set; }
        public int TipoFirma { get; set; }
        public OrdenCapturaFormato OrdenCapturaFormato { get; set; }
    }

    public class FirmaResponse
    {
        public string File { get; set; }
    }
}
