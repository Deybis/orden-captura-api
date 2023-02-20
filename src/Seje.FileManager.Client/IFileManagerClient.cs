using System;
using System.Threading.Tasks;

namespace Seje.FileManager.Client
{
    public interface IFileManagerClient
    {
        Task<bool> UploadFile(Models.Archivo model);
        Task<bool> UploadFile(Models.ArchivoExpediente model);
        Task<bool> CreateRoot(Models.DirectoryRoot model);
        Task<bool> CreateSubDirectory(Models.SubDirectory model);
        Task<Models.FileToken> GetUrl(Guid id);
    }
}
