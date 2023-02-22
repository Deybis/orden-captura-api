using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.Authorization.Service.Models
{
    public class RedisConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Instance { get; set; }
        public bool Expiration { get; set; }
        public int AbsoluteExpiration { get; set; }
        public int SlidingExpiration { get; set; }
    }
}
