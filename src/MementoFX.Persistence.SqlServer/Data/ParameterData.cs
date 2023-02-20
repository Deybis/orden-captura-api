using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoFX.Persistence.SqlServer.Data
{
    internal class ParameterData
    {
        public ParameterData(string name, object value, SqlDbType type)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            this.Name = name;
            this.Value = value;
            this.Type = type;
        }

        public string Name { get; }

        public SqlDbType Type { get; }

        public object Value { get; set; }

        public bool HasValue
        {
            get { return this.Value != null && this.Value != DBNull.Value; }
        }
    }
}
