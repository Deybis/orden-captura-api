using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Seje.Authorization.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Authorization.Service.Infrastructure
{
    public class PermissionService : IPermissionService
    {
        private readonly IDistributedCache cache;
        private DistributedCacheEntryOptions optionsCache;
        private RedisConfiguration redisConfiguration;
        private readonly DateTime dateTimeCurrent;
        private readonly IAuthorizationService authorizationService;

        public PermissionService(IAuthorizationService authorizationService,
            IDistributedCache cache,
            IOptions<Models.ConfigurationModel> options)
        {
            this.redisConfiguration = options.Value.RedisConfiguration;

            if (redisConfiguration.Expiration)
            {
                this.dateTimeCurrent = DateTime.Now;
                this.optionsCache = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(dateTimeCurrent.AddMinutes(redisConfiguration.AbsoluteExpiration))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(redisConfiguration.SlidingExpiration));
            }

            this.authorizationService = authorizationService;
            this.cache = cache;
        }


        public async Task<bool> ValidatePermission(string userName, string component, string controller, string action)
        {
            var key = $"{Constants.PrefixSys}{Constants.PermissionKey}{userName}_{component}";
            var strPermissions = await cache.GetStringAsync(key);

            List<Permission> permissions = string.IsNullOrEmpty(strPermissions) || strPermissions == "[]" ? 
                await LoadPermissionsAsync(userName, component) :
                JsonConvert.DeserializeObject<List<Permission>>(strPermissions);

            return permissions.Any(p => p.ActionName.ToLower() == action.ToLower() && p.ControllerName.ToLower() == controller.ToLower());
        }

        private async Task<List<Permission>> LoadPermissionsAsync(string userName, string component)
        {
            var result = await authorizationService.GetPermissionsBy(userName, component);
            var strResult = JsonConvert.SerializeObject(result);

            // Save cache
            var key = $"{Constants.PrefixSys}{Constants.PermissionKey}{userName}_{component}";
            if (redisConfiguration.Expiration) await cache.SetStringAsync(key, strResult, optionsCache);
            else await cache.SetStringAsync(key, strResult);

            return result;
        }

    }
}
