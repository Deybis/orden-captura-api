using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Services.Identity
{
    public class Configuration
    {
        public string Authority { get; set; }
        public string ApiName { get; set; }

        public List<string> ValidIssuers { get; set; }
        public bool ValidateIssuer { get; set; }

        /// <summary>
        /// This property is used for the ClaimCredential flow
        /// </summary>
        public IdentityInfo IdentityInfo { get; set; }
    }

    public class IdentityInfo
    {
        public string Authority { get; set; }
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Scopes { get; set; }
    }
}
