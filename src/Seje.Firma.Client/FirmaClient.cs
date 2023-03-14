using Entities.Shared.Model;
using Microsoft.Extensions.Logging;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Seje.Firma.Client
{
    public class FirmaClient : IFirmaDigitalClient
    {
        private readonly HttpClient client;
        private readonly ILogger<FirmaClient> _logger;
        public FirmaClient(HttpClient client, ILogger<FirmaClient> logger)
        {
            this.client = client;
            _logger = logger;
        }
        public async Task<Result<FirmaResponse>> Firmar(FirmaRequest model)
        {
            var result = new Result<FirmaResponse>(false, null, new FirmaResponse());
            try
            {
                string url = $"api/documentsign/sign/s?sistema=OrdenCaptura";
                var form = new MultipartFormDataContent
                {
                    { new StringContent(model.FirmaMode), "users[0].FirmaMode" },
                    { new StringContent(model.UserName), "users[0].UserName" },
                    { new StringContent(model.SignHeight), "users[0].SignHeight" },
                    { new StringContent(model.SignWidth), "users[0].SignWidth" },
                    { new StringContent(model.SignX), "users[0].SignX" },
                    { new StringContent(model.SignY), "users[0].SignY" },
                    { new StringContent(model.File), "file" }
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                HttpResponseMessage response = await client.PostAsync(url, form);
                response.EnsureSuccessStatusCode();
                result.Entity.File = await response.Content.ReadAsStringAsync();
                result.Success = response.IsSuccessStatusCode;
                return result;                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                _logger.LogInformation($"Ocurrio un error al firmar el documento en FirmaClient:{ex.Message}");
                return result;
            }
        }
    }
}
