using IdentityModel.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seje.Services.Identity.Infrastructure
{
    public class ProtectedApiBearerTokenHandler : DelegatingHandler
    {
        private readonly IAuthServerConnect _authServerConnect;
        private readonly ILogger _log;

        public ProtectedApiBearerTokenHandler(IAuthServerConnect authServerConnect)
        {
            _authServerConnect = authServerConnect;
            _log = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // request the access token
            var accessToken = await _authServerConnect.RequestClientCredentialsTokenAsync();

            _log.Warning(string.Format("------ Token: {0} ------", accessToken));

            // set the bearer token to the outgoing request as Authentication Header
            request.SetBearerToken(accessToken);

            // Proceed calling the inner handler, that will actually send the requestto our protected api
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
