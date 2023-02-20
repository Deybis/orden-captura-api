using Entities.Shared.Model;
using Newtonsoft.Json;
using Seje.OrdenCaptura.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Firma.Client
{
    public class FirmaClient : IFirmaDigitalClient
    {
        private readonly HttpClient client;
        public FirmaClient(HttpClient client)
        {
            this.client = client;
        }
        public async Task<Result<FirmaResponse>> Firmar(FirmaRequest model)
        {
            var result = new Result<FirmaResponse>(false, null, new FirmaResponse());
            try
            {
                string url = $"api/documentsign/sign/s?sistema=OrdenCaptura";
                var user = model.UserName == "remejia@poderjudicial.gob.hn" ? "amontano@poderjudicial.gob.hn" : "ejalvarado@poderjudicial.gob.hn"; //TODO:quitar cuando se agreguen los usuarios al componente de firma

                var request = new HttpRequestMessage();
                request.Content = new MultipartFormDataContent
                    {
                        new StringContent(model.FirmaMode)
                        {
                            Headers =
                            {
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "users[0].FirmaMode",
                                }
                            }
                        },
                        new StringContent(user)
                        {
                            Headers =
                            {
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "users[0].UserName",
                                }
                            }
                        },
                        new StringContent(model.SignHeight)
                        {
                            Headers =
                            {
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "users[0].SignHeight",
                                }
                            }
                        },
                        new StringContent(model.SignWidth)
                        {
                            Headers =
                            {
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "users[0].SignWidth",
                                }
                            }
                        },
                        new StringContent(model.SignX)
                        {
                            Headers =
                            {
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "users[0].SignX",
                                }
                            }
                        },
                        new StringContent(model.SignY)
                        {
                            Headers =
                            {
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "users[0].SignY",
                                }
                            }
                        },
                        new StringContent(model.File)
                        {
                            Headers =
                            {
                                ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                {
                                    Name = "file",
                                }
                            }
                        },
                };

                using (var response = await client.PostAsync(url,request.Content))
                {
                    response.EnsureSuccessStatusCode();
                    result.Entity.File = await response.Content.ReadAsStringAsync();
                }
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                return result;
            }

        }
    }
}
