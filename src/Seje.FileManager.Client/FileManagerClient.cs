using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Seje.FileManager.Client.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace Seje.FileManager.Client
{
    public class FileManagerClient : IFileManagerClient
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<FileManagerClient> logger;
        public IConfiguration Configuration { get; }
        public const string SystemName = "FileManagerSettings:SystemName";

        public FileManagerClient(HttpClient httpClient, ILogger<FileManagerClient> logger, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            Configuration = configuration;
        }

        public async Task<bool> CreateRoot(DirectoryRoot model)
        {
            var url = "/directory/root";
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, stringContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateSubDirectory(SubDirectory model)
        {
            var url = "/directory/sub-directory";
            var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(url, stringContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<FileToken> GetUrl(Guid id)
        {
            FileToken result = new FileToken();
            try
            {
                var url = $"file/token/{id}";
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var re = JsonConvert.DeserializeObject<FileToken>(stringResponse);

                    var bytes = await httpClient.GetByteArrayAsync(re.Url);
                    re.fileBase64String = Convert.ToBase64String(bytes);
                    result = re;
                }
                else
                {
                    logger.LogInformation("FileManagerClient:No se encontró el documento");
                    if (response.StatusCode != HttpStatusCode.NotFound)
                        response.EnsureSuccessStatusCode();
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }

        }

        public async Task<bool> UploadFile(Archivo model)
        {
            bool result = false;
            var systemName = Configuration.GetSection(SystemName);
            try
            {
                return await uploadFile(model);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Bad Request"))
                {
                    var successRoot = await CreateRoot(new DirectoryRoot { DirectoryId = Guid.NewGuid(), SystemIdentifier = systemName.Value });
                    if (successRoot) result = await uploadFile(model);
                    return result;
                }
                logger.LogInformation("FileManagerClient - UploadFile: " + ex.Message);
                return result;
            }           
        }

        private async Task<bool> uploadFile(Archivo model)
        {
            var url = "/file/upload";
            using var form = new MultipartFormDataContent();
            using var fs = File.OpenRead(model.FilePath);
            using var streamContent = new StreamContent(fs);
            using var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            form.Add(fileContent, "file", model.FileName);
            StringContent jsonPart = new(JsonConvert.SerializeObject(model));
            form.Add(jsonPart, "model");
            HttpResponseMessage response = await httpClient.PostAsync(url, form);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UploadFile(ArchivoExpediente model)
        {
            var url = "/file/upload-expediente";
            var fileName = string.Format("{0}.{1}", model.Id.ToString(), model.DocumentExtension.Replace(".", ""));
            var filePath = Path.Combine(model.FilePath, fileName);

            using (var form = new MultipartFormDataContent())
            {
                using (var fs = File.OpenRead(filePath))
                {
                    using (var streamContent = new StreamContent(fs))
                    {
                        using (var fileContent = new ByteArrayContent(await streamContent.ReadAsByteArrayAsync()))
                        {
                            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                            form.Add(fileContent, "file", model.FileName);
                            StringContent jsonPart = new StringContent(JsonConvert.SerializeObject(model));
                            form.Add(jsonPart, "model");
                            HttpResponseMessage response = await httpClient.PostAsync(url, form);
                            response.EnsureSuccessStatusCode();
                            return response.IsSuccessStatusCode;
                        }
                    }
                }
            }
        }
    }
}
