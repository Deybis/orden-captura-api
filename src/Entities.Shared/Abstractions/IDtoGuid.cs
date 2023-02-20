using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Shared.Abstractions
{
    public interface IDtoGuid<TUserKey> : IBase<Guid, TUserKey>
    {
    }
}
