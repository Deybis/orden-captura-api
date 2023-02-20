using MementoFX.Persistence.SqlServer.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoFX.Persistence.SqlServer.Data
{
    internal class TypeInfo
    {
        public TypeInfo(SqlDbType type, bool isNullable, bool isClass)
        {
            this.Type = type;
            this.IsNullable = isNullable;
            this.IsClass = isClass;
        }

        public SqlDbType Type { get; }

        public bool IsNullable { get; }

        public bool IsClass { get; }

        public override string ToString()
        {
            var type = this.Type.ToString().ToUpper();
            if (TypeHelper.IsVariableBinaryOrCharType(this.Type))
            {
                type += "(MAX)";
            }

            return type;
        }
    }
}
