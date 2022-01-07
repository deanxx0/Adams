using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class ImageInfoController : ControllerBase
    {
        DbClient _client;
        public ImageInfoController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/imageinfos")]
        public async Task<IActionResult> Add(string projectId, [FromBody] ImageInfo entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if(project == null) return NotFound("No project id");
            // item id check
            var items = _client.GetProjectDB(projectId).Items();
            var item = items.Find(x => x.Id == entity.ItemId).FirstOrDefault();
            if(item == null) return NotFound("No item id");
            // channel id check
            var inputchannels = _client.GetProjectDB(projectId).InputChannels();
            var inputchannel = inputchannels.Find(x => x.Id == entity.ChannelId).FirstOrDefault();
            if (inputchannel == null) return NotFound("No channel id");

            var newImageInfo = new ImageInfo(entity.ItemId, entity.ChannelId, entity.OriginalFilePath, true);
            if (entity.Id != null)
                newImageInfo.SetId(entity.Id);

            var imageInfos = _client.GetProjectDB(projectId).ImageInfos();
            imageInfos.InsertOne(newImageInfo);
            return Ok(newImageInfo);
        }

        [HttpGet("projects/{projectId}/imageinfos/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var imageInfos = _client.GetProjectDB(projectId).ImageInfos();
            var count = imageInfos.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/imageinfos/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            if (pageNo < 1) pageNo = 1;
            var imageInfos = _client.GetProjectDB(projectId).ImageInfos();
            var res = imageInfos.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/imageinfos")]
        public async Task<IActionResult> Get(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var imageInfos = _client.GetProjectDB(projectId).ImageInfos();
            var res = imageInfos.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/imageinfos")]
        public async Task<IActionResult> Update(string projectId, [FromBody] ImageInfo entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");
            // item id check
            var items = _client.GetProjectDB(projectId).Items();
            var item = items.Find(x => x.Id == entity.ItemId).FirstOrDefault();
            if (item == null) return NotFound("No item id");
            // channel id check
            var inputchannels = _client.GetProjectDB(projectId).InputChannels();
            var inputchannel = inputchannels.Find(x => x.Id == entity.ChannelId).FirstOrDefault();
            if (inputchannel == null) return NotFound("No channel id");

            var imageInfos = _client.GetProjectDB(projectId).ImageInfos();
            imageInfos.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
