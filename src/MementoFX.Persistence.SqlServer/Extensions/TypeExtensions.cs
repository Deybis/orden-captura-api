using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoFX.Persistence.SqlServer.Extensions
{
    public static class TypeExtensions
    {
        public static string GetFullTypeAndAssemblyName(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.FullName + ", " + type.Assembly.FullName;
        }
    }
}
