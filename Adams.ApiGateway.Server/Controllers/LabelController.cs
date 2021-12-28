using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class LabelController : ControllerBase
    {
        DbClient _client;
        public LabelController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/labels")]
        public async Task<IActionResult> Add(string projectId, [FromBody] Label entity)
        {
            var data = entity.Data ?? "";
            var newLabel = new Label(entity.ItemId, entity.ClassId, data);
            if (entity.Id != null)
                newLabel.SetId(entity.Id);

            var labels = _client.GetProjectDB(projectId).Labels();
            labels.InsertOne(newLabel);
            return Ok(newLabel);
        }

        [HttpGet("projects/{projectId}/labels/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            var labels = _client.GetProjectDB(projectId).Labels();
            var count = labels.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/labels/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            if (pageNo < 1) pageNo = 1;
            var labels = _client.GetProjectDB(projectId).Labels();
            var res = labels.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/labels")]
        public async Task<IActionResult> Get(string projectId)
        {
            var labels = _client.GetProjectDB(projectId).Labels();
            var res = labels.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/labels")]
        public async Task<IActionResult> Update(string projectId, [FromBody] Label entity)
        {
            var labels = _client.GetProjectDB(projectId).Labels();
            labels.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }

        [HttpDelete("projects/{projectId}/labels/{labelId}")]
        public async Task<IActionResult> Delete(string projectId, string labelId)
        {
            var labels = _client.GetProjectDB(projectId).Labels();
            var res = labels.DeleteOne(x => x.Id == labelId);
            return Ok(res);
        }
    }
}
