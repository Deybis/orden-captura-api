using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MementoFX.Persistence.SqlServer.Extensions
{
    internal static class SqlCommandExtensions
    {
        public static void AddRange(this SqlParameterCollection parameterCollection, IEnumerable<SqlParameter> parameters)
        {
            if (parameterCollection == null)
            {
                throw new ArgumentNullException(nameof(parameterCollection));
            }

            if (parameters != null && parameters.Count() > 0)
            {
                foreach (var parameter in parameters)
                {
                    parameterCollection.Add(parameter);
                }
            }
        }
    }
}
