using System;
using System.Collections.Generic;
using System.Text;

namespace Seje.FileManager.Client.Models
{
    public class FileModel : IFileModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public string DocumentExtension { get; set; }
        public string MIME_Type { get; set; }
        public string UserName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string SourceSystem { get; set; }
        public string FilePath { get; set; }
    }
}
