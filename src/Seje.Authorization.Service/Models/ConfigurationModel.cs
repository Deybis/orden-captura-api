using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.Authorization.Service.Models
{
    public class ConfigurationModel
    {
        public string Host { get; set; }
        public string Component { get; set; }
        public bool AlreadyRedisConfiguration { get; set; }
        public RedisConfiguration RedisConfiguration { get; set; }
    }
}
