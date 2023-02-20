using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api
{
    public class ProtectedApiBearerTokenHandler : DelegatingHandler
    {
        private readonly IAuthServerConnect _authServerConnect;
        private readonly ILogger<ProtectedApiBearerTokenHandler> logger;

        public ProtectedApiBearerTokenHandler(IAuthServerConnect authServerConnect, ILogger<ProtectedApiBearerTokenHandler> logger)
        {
            _authServerConnect = authServerConnect;
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // request the access token
            var accessToken = await _authServerConnect.RequestClientCredentialsTokenAsync();
            logger.LogInformation("Token: " + accessToken);
            // set the bearer token to the outgoing request as Authentication Header
            request.SetBearerToken(accessToken);

            // Proceed calling the inner handler, that will actually send the requestto our protected api
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
