using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class StorageController : ControllerBase
    {
        DbClient _client;
        string _storagePath = "D:\\2022\\adams\\project\\FileStorage";
        public StorageController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/items/{itemId}imageinfos/{imageInfoId}/upload")]
        public async Task<IActionResult> UploadImage(string projectId, string itemId, string imageInfoId, IFormFile file)
        {
            // projectid check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if(project == null) return NotFound();
            // itemId check
            var items = _client.GetProjectDB(projectId).Items();
            var item = items.Find(x => x.Id == itemId).FirstOrDefault();
            if (item == null) return NotFound();
            // imageInfoId check
            var imageInfos = _client.GetProjectDB(projectId).ImageInfos();
            var imageInfo = imageInfos.Find(x => x.Id == imageInfoId).FirstOrDefault();
            if (imageInfo == null) return NotFound();
            //original file name check
            //var ori = Path.GetFileName(imageInfo.OriginalFilePath);
            //var filename = file.FileName;
            //if (ori != filename)
            //    return BadRequest("Not matched file name");

            // file upload save
            try
            {
                var fileType = file.ContentType.Split('/'); // png, jpg, bmp, tiff
                if (file.Length > 0)
                {
                    var filePath = $"{_storagePath}\\{projectId}\\{imageInfoId}" + "." + fileType[1];
                    if (System.IO.File.Exists(filePath))
                    {
                        return BadRequest("File already exist");
                    }
                    if (!Directory.Exists($"{_storagePath}\\{projectId}\\"))
                    {
                        Directory.CreateDirectory($"{_storagePath}\\{projectId}\\");
                    }

                    using (FileStream fileStream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        // add server image path to imageInfo
                        imageInfo.SetValue("OriginalFilePath", filePath);
                        return Ok($"{_storagePath}\\{projectId}\\{imageInfoId}" + "." + fileType[1]);
                    }
                }
                else
                {
                    return BadRequest("Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("projects/{projectId}/items/{itemId}imageinfos/{imageInfoId}/download")]
        public async Task<IActionResult> DownloadImage(string projectId, string itemId, string imageInfoId)
        {
            // projectid check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound();
            // itemId check
            var items = _client.GetProjectDB(projectId).Items();
            var item = items.Find(x => x.Id == itemId).FirstOrDefault();
            if (item == null) return NotFound();
            // imageInfoId check
            var imageInfos = _client.GetProjectDB(projectId).ImageInfos();
            var imageInfo = imageInfos.Find(x => x.Id == imageInfoId).FirstOrDefault();
            if (imageInfo == null) return NotFound();
            // file download
            try
            {
                if (!Directory.Exists($"{_storagePath}\\{projectId}\\"))
                {
                    return null;
                }
                else
                {
                    var files = Directory.GetFiles($"{_storagePath}\\{projectId}");
                    var targetFilePath = files.Where(x => x.Contains(imageInfoId)).FirstOrDefault();
                    var targetFile = new FileInfo(targetFilePath);
                    return File(System.IO.File.ReadAllBytes(targetFilePath), "application/octet-stream", targetFile.Name);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("train/{trainId}/download/{downloadPath}")]
        public async Task<IActionResult> DownloadModel(string trainId, string downloadPath)
        {
            return Ok();
        }
    }
}
