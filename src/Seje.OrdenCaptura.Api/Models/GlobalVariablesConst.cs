using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seje.OrdenCaptura.Api.Models
{
    public class AppSettings
    {
        // Authorization
        public const string API_URL_AUTHORIZATION = "AuthorizationSettings:Host";
        public const string REDIS_CONFIGURATION = "AuthorizationSettings:RedisConfiguration";
        public const string COMPONENT_AUTHORIZATION = "AuthorizationSettings:Component";
    }
}
