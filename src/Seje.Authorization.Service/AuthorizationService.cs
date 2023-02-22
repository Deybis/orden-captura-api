using Seje.Authorization.Service.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Authorization.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly HttpClient client;

        public AuthorizationService(HttpClient client)
        {
            this.client = client;
        }

        public Task<List<Permission>> GetPermissionsBy(string userName, string componentId, string target = "")
        {
            string url = $"api/query/user/{userName}/component/{componentId}/permissions?target={target}";
            return Get<List<Permission>>(url);
        }

        public Task<List<string>> GetRoles(string userName)
        {
            string url = $"api/user/{userName}/roles";
            return Get<List<string>>(url);
        }

        public Task<List<string>> GetRolesBy(string userName, string componentId)
        {
            string url = $"api/query/user/{userName}/component/{componentId}/roles";
            return Get<List<string>>(url);
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
            }
            return result;
        }

    }
}
