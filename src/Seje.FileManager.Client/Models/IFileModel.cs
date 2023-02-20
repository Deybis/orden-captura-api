using System;

namespace Seje.FileManager.Client.Models
{
    public interface IFileModel
    {
        Guid Id { get; }
        string FileName { get; }
        string Description { get; }
        string DocumentType { get; }
        string DocumentExtension { get; }
        string MIME_Type { get; }
        string UserName { get; }
        DateTime TimeStamp { get; }
        string SourceSystem { get; }
        string FilePath { get; }
    }
}