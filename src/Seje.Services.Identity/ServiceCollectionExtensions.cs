using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Seje.Services.Identity
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, Action<Configuration> configureOptions)
        {
            if (configureOptions == null) throw new ArgumentNullException(nameof(configureOptions));
            services.Configure(configureOptions);

            var config = new Configuration();
            configureOptions(config);

            if (config.IdentityInfo != null)
            {
                services.AddSingleton<IDiscoveryCache>(r =>
                {
                    var factory = r.GetRequiredService<IHttpClientFactory>();
                    return new DiscoveryCache(config.IdentityInfo.Authority, () => factory.CreateClient());
                });

                services.AddScoped<Infrastructure.IAuthServerConnect, Infrastructure.AuthServerConnect>();
                services.AddTransient<Infrastructure.ProtectedApiBearerTokenHandler>();
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = config.Authority;
                    options.Audience = config.ApiName;

                    options.TokenValidationParameters.ValidIssuers = config.ValidIssuers;
                    options.TokenValidationParameters.ValidateIssuer = config.ValidateIssuer;

                    options.SecurityTokenValidators.Clear();

                    options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler
                    {
                        MapInboundClaims = false,
                    });

                    options.TokenValidationParameters.NameClaimType = "name";
                    options.TokenValidationParameters.RoleClaimType = "role";

                    options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                    options.RequireHttpsMetadata = true;
                });
            return services;
        }

        public static Configuration AddSection(this Configuration configureOptions, IConfigurationSection section)
        {
            var settings = section.Get<Configuration>();
            configureOptions.ApiName = settings.ApiName;
            configureOptions.Authority = settings.Authority;
            configureOptions.IdentityInfo = settings.IdentityInfo;
            configureOptions.ValidateIssuer = settings.ValidateIssuer;
            configureOptions.ValidIssuers = settings.ValidIssuers;
            return configureOptions;
        }
    }
}
