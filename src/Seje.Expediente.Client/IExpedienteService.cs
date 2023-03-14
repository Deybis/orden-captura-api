using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Expediente.Client
{
    public interface IExpedienteService
    {
        Task<Models.Expediente> GetExpediente(string numeroExpediente);
        Task<Models.Expediente> ListarDelitos(string alternateKey);
        Task<Models.Expediente> GetExpediente(string numeroExpediente, string authorizationToken, string authorizationMethod = "Bearer");
    }
}
