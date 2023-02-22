using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Authorization.Service
{
    public interface IAuthorizationService
    {
        Task<List<Models.Permission>> GetPermissionsBy(string userName, string componentId, string target = "");
        // Task<List<Models.Permission>> GetUserInfo(string userName);
        public Task<List<string>> GetRolesBy(string userName, string componentId);
        public Task<List<string>> GetRoles(string userName);
    }
}
