using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class DatasetController : ControllerBase
    {
        DbClient _client;
        public DatasetController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/datasets")]
        public async Task<IActionResult> Add(string projectId, [FromBody] Dataset entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var description = entity.Description ?? "";
            var newDataset = new Dataset(entity.Name, description, entity.Type, true);
            if (entity.Id != null)
                newDataset.SetId(entity.Id);

            var datasets = _client.GetProjectDB(projectId).Datasets();
            datasets.InsertOne(newDataset);
            return Ok(newDataset);
        }

        [HttpGet("projects/{projectId}/datasets/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var datasets = _client.GetProjectDB(projectId).Datasets();
            var count = datasets.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/datasets/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            if (pageNo < 1) pageNo = 1;
            var datasets = _client.GetProjectDB(projectId).Datasets();
            var res = datasets.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/datasets")]
        public async Task<IActionResult> Get(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var datasets = _client.GetProjectDB(projectId).Datasets();
            var res = datasets.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/datasets")]
        public async Task<IActionResult> Update(string projectId, [FromBody] Dataset entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var datasets = _client.GetProjectDB(projectId).Datasets();
            datasets.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
