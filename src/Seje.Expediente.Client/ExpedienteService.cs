using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Seje.Expediente.Client
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly HttpClient client;
        private readonly ILogger _log;

        public ExpedienteService(HttpClient client)
        {
            this.client = client;
            _log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public async Task<Models.Expediente> GetExpediente(string numeroExpediente, string authorizationToken, string authorizationMethod = "Bearer")
        {
            string url = $"/api/v1/expedientes/exp/{numeroExpediente}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            var response = await client.SendAsync(requestMessage);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }
            if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
            }
            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Models.Expediente>(stringResponse);
        }

        public Task<Models.Expediente> GetExpediente(string numeroExpediente)
        {
            //string url = $"/api/v1/expedientes/exp/{numeroExpediente}";
            string url = $"api/v1/expedientes/BuscarExpediente/{numeroExpediente}";
            return Get<Models.Expediente>(url);
        }

        public Task<Models.Expediente> ListarDelitos(string alternatedKey)
        {
            string url = $"api/v1/delitos/ListaDelitos/{alternatedKey}";
            return Get<Models.Expediente>(url);
        }

        private async Task<T> Get<T>(string url)
            where T : class
        {
            HttpResponseMessage response = await client.GetAsync(url);
            T result = null;
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringResponse);
            }
            else
            {
                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    response.EnsureSuccessStatusCode();
                }

                var statusCode = response.StatusCode;
                var message = response.ReasonPhrase;

                var responseContent = response.Content;
                var messageDetail = (responseContent == null)
                                        ? null
                                        : ((StringContent)responseContent).ReadAsStringAsync().Result;

                _log.Warning(string.Format("---------- Error {0} ----------", message));
                _log.Warning(string.Format("---------- Detalle {0} ----------", messageDetail));
            }
            return result;
        }
    }
}
