using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Authorization.Service.Infrastructure
{
    public interface IPermissionService
    {
        public Task<bool> ValidatePermission(string userName, string component, string controller, string action);
    }
}
