using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Seje.Authorization.Service.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.Authorization.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services, Action<ConfigurationModel> configureOptions)
        {
            services.Configure(configureOptions);

            ConfigurationModel options = new ConfigurationModel();
            configureOptions(options);

            if(!options.AlreadyRedisConfiguration)
                services.AddRedisConfiguration(options);

            services.AddHttpClients(options);

            services.AddScoped<Infrastructure.IPermissionService, Infrastructure.PermissionService>();
            services.AddTransient<Infrastructure.IUserService, Infrastructure.UserService>();
            services.AddScoped<Filters.PermissionFilter>();
            return services;
        }

        private static void AddHttpClients(this IServiceCollection services, ConfigurationModel configuration)
        {
            // Authorization
            services.AddHttpClient<IAuthorizationService, AuthorizationService>(config =>
            {
                config.BaseAddress = new Uri(configuration.Host);
                config.Timeout = TimeSpan.FromSeconds(180);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(4, retryAttempt))));
        }

        private static void AddRedisConfiguration(this IServiceCollection services, ConfigurationModel configuration)
        {
            RedisConfiguration redisConfiguration = configuration.RedisConfiguration;
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{redisConfiguration.Host}:{redisConfiguration.Port}";
                options.InstanceName = redisConfiguration.Instance;
            });
        }
    }
}
