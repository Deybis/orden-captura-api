using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Seje.Authorization.Service.Infrastructure
{
    public interface IUserService
    {
        public Task<List<string>> GetRoles(string userName);
    }
}
