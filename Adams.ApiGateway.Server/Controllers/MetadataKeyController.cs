using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class MetadataKeyController : ControllerBase
    {
        DbClient _client;
        public MetadataKeyController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/metadatakeys")]
        public async Task<IActionResult> Add(string projectId, [FromBody] MetadataKey entity)
        {
            var isEnabled = entity.IsEnabled == null ? true : entity.IsEnabled.Value;
            var newEntity = entity.Id == null ?
                new MetadataKey(entity.Key, entity.Description, entity.Type, isEnabled) :
                new MetadataKey(entity.Id, entity.Key, entity.Description, entity.Type, isEnabled);

            var metadatakeys = _client.GetProjectDB(projectId).MetadataKeys();
            metadatakeys.InsertOne(newEntity);
            return Ok(newEntity);
        }

        [HttpGet("projects/{projectId}/metadatakeys/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            var metadatakeys = _client.GetProjectDB(projectId).MetadataKeys();
            var count = metadatakeys.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/metadatakeys/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            if (pageNo < 1) pageNo = 1;
            var metadatakeys = _client.GetProjectDB(projectId).MetadataKeys();
            var res = metadatakeys.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/metadatakeys")]
        public async Task<IActionResult> Get(string projectId)
        {
            var metadatakeys = _client.GetProjectDB(projectId).MetadataKeys();
            var res = metadatakeys.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/metadatakeys")]
        public async Task<IActionResult> Update(string projectId, [FromBody] MetadataKey entity)
        {
            var metadatakeys = _client.GetProjectDB(projectId).MetadataKeys();
            metadatakeys.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
