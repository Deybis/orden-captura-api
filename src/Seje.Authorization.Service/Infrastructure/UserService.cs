using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Seje.Authorization.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Authorization.Service.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly IDistributedCache cache;
        private DistributedCacheEntryOptions optionsCache;
        private RedisConfiguration redisConfiguration;
        private readonly DateTime dateTimeCurrent;
        private readonly IAuthorizationService authorizationService;

        public UserService(IAuthorizationService authorizationService,
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

        public async Task<List<string>> GetRoles(string userName)
        {
            var key = $"{Constants.PrefixSys}{Constants.RolesKey}{userName}";
            var strRoles = await cache.GetStringAsync(key);

            List<string> roles = string.IsNullOrEmpty(strRoles) ?
                await LoadRolessAsync(userName) :
                JsonConvert.DeserializeObject<List<string>>(strRoles);

            return roles;
        }

        private async Task<List<string>> LoadRolessAsync(string userName)
        {
            var result = await authorizationService.GetRoles(userName);
            var strResult = JsonConvert.SerializeObject(result);

            if (result.Count > 0)
            {
                // Save cache
                var key = $"{Constants.PrefixSys}{Constants.RolesKey}{userName}";
                if (redisConfiguration.Expiration) await cache.SetStringAsync(key, strResult, optionsCache);
                else await cache.SetStringAsync(key, strResult);
            }

            return result;
        }
    }
}
