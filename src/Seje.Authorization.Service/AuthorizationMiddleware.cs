using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;
using System;
using Seje.Authorization.Service.Models;
using System.Linq;

namespace Seje.Authorization.Service
{
    public class AuthorizationMiddleware
    {
        private readonly IDistributedCache cache;
        private DistributedCacheEntryOptions optionsCache;
        private readonly RedisConfiguration redisConfiguration;
        private DateTime dateTimeCurrent;
        private readonly ConfigurationModel configurationModel;

        private readonly IAuthorizationService service;


        public AuthorizationMiddleware(RequestDelegate next,
            IAuthorizationService service,
            IDistributedCache cache,
            IOptions<Models.ConfigurationModel> options)
        {
            this.service = service;
            this.cache = cache;
            this.configurationModel = options.Value;
            this.redisConfiguration = options.Value.RedisConfiguration;            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var routeValues = context.GetRouteData().Values;
            var action = routeValues["action"] as string;

            if(action.ToLower() == "configure")
            {
                var userName = context.Request.Query["userName"].ToString();
                var component = configurationModel.Component;
                var target = context.Request.Query["target"].ToString();

                var result = await service.GetPermissionsBy(userName, component, target);

                var strResult = JsonConvert.SerializeObject(result);

                // Save cache
                ConfigureRedisOptions();
                var key = $"{Constants.PrefixSys}{Constants.PermissionKey}{userName}_{component}";
                if (redisConfiguration.Expiration) await cache.SetStringAsync(key, strResult, optionsCache);
                else await cache.SetStringAsync(key, strResult);

                if (result.Any(p => p.Target == "APP"))
                {
                    context.Response.ContentType = "application/json";
                    var appPermission = result.Where(p => p.Target == "APP").ToList();
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(appPermission));
                }
                
                // context.Response.StatusCode = 200;
            }
            else if(action.ToLower() == "navigation-menu")
            {
                var userName = context.Request.Query["userName"].ToString();
                var component = configurationModel.Component;

                var result = await service.GetPermissionsBy(userName, component, "APP");
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
            else if (action.ToLower() == "roles-user-component")
            {
                var userName = context.Request.Query["userName"].ToString();
                var component = configurationModel.Component;

                var result = await service.GetRolesBy(userName, component);
                var strResult = JsonConvert.SerializeObject(result);

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(strResult);
            }
            else if(action.ToLower() == "roles")
            {
                var userName = context.Request.Query["userName"].ToString();
                var component = configurationModel.Component;

                var result = await service.GetRolesBy(userName, component);
                var strResult = JsonConvert.SerializeObject(result);

                if (result.Count > 0)
                {
                    // Save cache
                    ConfigureRedisOptions();
                    var key = $"{Constants.PrefixSys}{Constants.RolesKey}{userName}";
                    if (redisConfiguration.Expiration) await cache.SetStringAsync(key, strResult, optionsCache);
                    else await cache.SetStringAsync(key, strResult);
                }

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(strResult);
            }
            else
            {
                context.Response.StatusCode = 404;
            }
            
            return;
        }

        private void ConfigureRedisOptions()
        {
            if (redisConfiguration.Expiration)
            {
                this.dateTimeCurrent = DateTime.Now;
                this.optionsCache = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(dateTimeCurrent.AddMinutes(redisConfiguration.AbsoluteExpiration))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(redisConfiguration.SlidingExpiration));
            }
        }
    }

    public static class CustomMiddlewareExtensions
    {
        const string controller = "authorization";
        public static IEndpointConventionBuilder MapAuthorizationEndpoint(this IEndpointRouteBuilder endpoints)
        {
            var pattern = controller + "/{action}/{id?}";
            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<AuthorizationMiddleware>()
                .Build();

            return endpoints.Map(pattern, pipeline);
        }
    }
}
