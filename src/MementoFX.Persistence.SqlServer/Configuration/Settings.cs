using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MementoFX.Persistence.SqlServer.Configuration
{
    public class Settings
    {
        public Settings(string connectionString, bool autoIncrementalTableMigrations = true, bool useCompression = false, bool useSingleTable = false, string schemaName = "StreamEvents")
        {
            this.ConnectionString = connectionString;
            this.AutoIncrementalTableMigrations = autoIncrementalTableMigrations;
            this.UseCompression = useCompression;
            this.UseSingleTable = useSingleTable;
            this.SchemaName = schemaName;
        }

        public string ConnectionString { get; }

        public bool AutoIncrementalTableMigrations { get; }

        public bool UseCompression { get; }

        public bool UseSingleTable { get; }

        public string SchemaName { get; }
    }
}
