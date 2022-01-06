using NAVIAIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Adams.Client
{
    public class StorageService : IStorageService
    {
        public HttpClient _http;
        public StorageService(HttpClient http)
        {
            _http = http;
        }

        public void DownloadImage(IProjectService projectService, string itemId, string imageInfoId, string downloadPath)
        {
            try
            {
                var projectId = projectService.Entity.Id;
                var response = _http.GetAsync($"projects/{projectId}/items/{itemId}imageinfos/{imageInfoId}/download").Result;
                response.EnsureSuccessStatusCode();
                var fileName = response.Content.Headers.ContentDisposition.FileName;
                using var ms = response.Content.ReadAsStreamAsync().Result;
                var fileInfo = new FileInfo($"{downloadPath}\\{fileName}");
                using var fs = File.Create(fileInfo.FullName);
                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(fs);
                Console.WriteLine("download OK!!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DownloadModelFile(IProjectService projectService, string trainId, string downloadPath)
        {
            
        }

        public void UploadImage(IProjectService projectService, string origialPath, string itemId, string imageInfoId)
        {
            try
            {
                var projectId = projectService.Entity.Id;
                var filePath = Path.Combine(origialPath);
                var fileBtyes = File.ReadAllBytes(filePath);
                var byteArrayContent = new ByteArrayContent(fileBtyes);
                var postResponse = _http.PostAsync($"projects/{projectId}/items/{itemId}imageinfos/{imageInfoId}/upload", new MultipartFormDataContent
                {
                    {byteArrayContent}
                }).Result;
                Console.WriteLine(postResponse);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
