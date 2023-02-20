using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api
{
    public class AuthServerConnect : IAuthServerConnect
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscoveryCache _discoveryCache;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;
        private const string tokenCacheKey = "IDENTITY_TOKEN";

        public AuthServerConnect(
            HttpClient httpClient,
            IConfiguration config,
            IDiscoveryCache discoveryCache,
            IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _config = config;
            _discoveryCache = discoveryCache;
            _memoryCache = memoryCache;
        }

        public Task<string> RequestClientCredentialsTokenAsync()
        {
            return _memoryCache.GetOrCreateAsync<string>(tokenCacheKey, async entry =>
            {
                var tokenResponse = await GetTokenAsync();
                var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResponse.ExpiresIn);
                entry.SetAbsoluteExpiration(expiresAt);
                return tokenResponse.AccessToken;
            });
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            var endPointDiscovery = await _discoveryCache.GetAsync();
            if (endPointDiscovery.IsError)
            {
                throw new HttpRequestException("Algo salió mal al conectarse al AuthServer Token Endpoint.");
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = endPointDiscovery.TokenEndpoint,
                ClientId = _config["IdentityInfo:Id"],
                ClientSecret = _config["IdentityInfo:Secret"],
                Scope = _config["IdentityInfo:Scopes"]
            });

            if (tokenResponse.IsError)
            {
                throw new HttpRequestException("Algo salió mal al solicitar Token al AuthServer. " + tokenResponse.Error);
            }
            Debug.WriteLine("token " + tokenResponse.AccessToken);
            return tokenResponse;
        }
    }
}
