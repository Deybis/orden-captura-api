using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Seje.Services.Identity.Infrastructure
{
    public class AuthServerConnect : IAuthServerConnect
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscoveryCache _discoveryCache;
        private readonly IMemoryCache _memoryCache;
        private readonly IdentityInfo _identityInfo;
        private const string tokenCacheKey = "IDENTITY_TOKEN";

        public AuthServerConnect(
            HttpClient httpClient,
            IOptions<Configuration> config,
            IDiscoveryCache discoveryCache,
            IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _identityInfo = config.Value.IdentityInfo;
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
                ClientId = _identityInfo.Id,
                ClientSecret = _identityInfo.Secret,
                Scope = _identityInfo.Scopes
            });

            if (tokenResponse.IsError)
            {
                throw new HttpRequestException("Algo salió mal al solicitar Token al AuthServer. " + tokenResponse.Error);
            }

            return tokenResponse;
        }
    }
}
