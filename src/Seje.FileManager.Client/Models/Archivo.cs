using System;

namespace Seje.FileManager.Client.Models
{
    public class Archivo : FileModel, IFileModel
    {
        public Guid DirectoryId { get; set; }
        public string Path { get; set; }
    }
}
