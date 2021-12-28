using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NAVIAIServices.Entities;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class ItemController : ControllerBase
    {
        DbClient _client;
        public ItemController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/items")]
        public async Task<IActionResult> Add(string projectId, [FromBody] Item entity)
        {
            var isEnabled = entity.IsEnabled == null ? true : entity.IsEnabled.Value;
            var newEntity = entity.Id == null ?
                new Item(entity.Tag, isEnabled) :
                new Item(entity.Id, entity.Tag, isEnabled);

            var items = _client.GetProjectDB(projectId).Items();
            items.InsertOne(newEntity);
            return Ok(newEntity);
        }

        [HttpGet("projects/{projectId}/items/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            var items = _client.GetProjectDB(projectId).Items();
            var count = items.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/items/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            if (pageNo < 1) pageNo = 1;
            var items = _client.GetProjectDB(projectId).Items();
            var res = items.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/items")]
        public async Task<IActionResult> Get(string projectId)
        {
            var items = _client.GetProjectDB(projectId).Items();
            var res = items.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/items")]
        public async Task<IActionResult> Update(string projectId, [FromBody] Item entity)
        {
            var items = _client.GetProjectDB(projectId).Items();
            items.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
