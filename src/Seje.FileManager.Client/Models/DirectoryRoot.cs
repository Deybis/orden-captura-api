using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.FileManager.Client.Models
{
    public class DirectoryRoot
    {
        public Guid DirectoryId { get; set; }
        public string SystemIdentifier { get; set; }
        public string MetaData { get; set; }
    }
}
