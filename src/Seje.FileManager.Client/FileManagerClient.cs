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

        public FileManagerClient(HttpClient httpClient, ILogger<FileManagerClient> logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
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
            var url = $"file/token/{id}";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            FileToken result = new FileToken();
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
                if (response.StatusCode != HttpStatusCode.NotFound)
                    response.EnsureSuccessStatusCode();
            }
            return result;
        }

        public async Task<bool> UploadFile(Archivo model)
        {
            var url = "/file/upload";
            try
            {
                using (var form = new MultipartFormDataContent())
                {
                    using (var fs = File.OpenRead(model.FilePath))
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
            catch (Exception ex)
            {
                logger.LogInformation("OrdenCaptura: " + ex.Message);
            }
            return false;
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
