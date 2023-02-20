using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Seje.OrdenCaptura.Api
{
    public static class AuthExtensions
    {
        private const string AUTH_SETTINGS_AUTHORITY = "AuthSettings:Authority";
        private const string AUTH_SETTINGS_SELF_AUTHORITY = "IdentityInfo:Authority";
        private const string AUTH_SETTINGS_APINAME = "AuthSettings:ApiName";

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var authority = configuration.GetValue<string>(AUTH_SETTINGS_AUTHORITY);
            var apiName = configuration.GetValue<string>(AUTH_SETTINGS_APINAME);
            services.AddAuthorization();

            services.AddSingleton<IDiscoveryCache>(r =>
            {
                var factory = r.GetRequiredService<IHttpClientFactory>();
                return new DiscoveryCache(configuration.GetValue<String>(AUTH_SETTINGS_SELF_AUTHORITY), () => factory.CreateClient());
            });

            services.AddScoped<IAuthServerConnect, AuthServerConnect>();
            services.AddTransient<ProtectedApiBearerTokenHandler>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authority;
                    options.ApiName = apiName;
                    options.NameClaimType = "name";
                    options.RoleClaimType = "role";
                    options.JwtBackChannelHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                });

            return services;
        }
    }
}
