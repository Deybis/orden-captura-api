using Entities.Shared.Model;
using System.IO;

namespace Seje.OrdenCaptura.Api.Utils
{
    public static class Util
    {
        public static string GetTemplate(string fileName,OrdenCapturaFormato formato,string nombreInstitucion,string qr,string logo)
        {
            string filePath = Path.Combine($"Templates/{fileName}");
            string template = File.ReadAllText(filePath)
            .Replace("$NumeroExpediente$", formato.NumeroExpediente)
            .Replace("$NumeroOrdenCaptura$", formato.NumeroOrdenCaptura)
            .Replace("$DespachoSala$", formato.Numerojuez)
            .Replace("$NumeroFormato$", formato.NumeroFormato)
            .Replace("$Juzgado$", formato.Juzgado)
            .Replace("$Ciudad$", formato.Ciudad)
            .Replace("$FechaEmision$", formato.FechaEmision)
            .Replace("$NombreInstitucionPolicial$", nombreInstitucion)
            .Replace("$NombreImputado$", formato.ExpedienteDetalle.NombreImputado)
            .Replace("$NacionalidadImputado$", formato.ExpedienteDetalle.NacionalidadImputado)
            .Replace("$DniImputado$", formato.ExpedienteDetalle.DniImputado)
            .Replace("$EdadImputado$", formato.ExpedienteDetalle.EdadImputado)
            .Replace("$DomicilioImputado$", formato.ExpedienteDetalle.DomicilioImputado)
            .Replace("$Delitos$", formato.ExpedienteDetalle.Delitos)
            .Replace("$Victimas$", formato.ExpedienteDetalle.Victimas)
            .Replace("$NombreJuez$", formato.Juez.NombreCompleto)
            .Replace("$PuestoJuez$", formato.Juez.Puesto)
            .Replace("$NombreSecretario$", formato.Secretario.NombreCompleto)
            .Replace("$PuestoSecretario$", formato.Secretario.Puesto)
            .Replace("$QR$", qr)
            .Replace("$LOGO$", logo);
            return template;
        }   
   
        public static string CleanBase64String(string base64String)
        {
            base64String = base64String.Replace("data:application/pdf;base64,", "");
            base64String = base64String.Replace("data:image/png;base64,", "");
            return base64String;
        }
    }
}
