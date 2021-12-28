using Adams.ApiGateway.Server.Auth;
using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NAVIAIServices.Entities;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class ProjectController : ControllerBase
    {

        DbClient _client;
        public ProjectController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects")]
        public async Task<IActionResult> Add([FromBody] Project project)
        {
            var newProject = new Project(project.AIType, project.Name, project.Description);
            if (project.Id != null)
                newProject.SetId(project.Id);

            _client.Projects.InsertOne(newProject);
            _client.GetProjectDB(newProject.Id).Projects().InsertOne(project);

            return Ok(newProject);
        }

        [HttpGet("projects/contains/{id}")]
        public async Task<IActionResult> Contains(string id)
        {
            var project = _client.Projects.Find(x => x.Id == id).FirstOrDefault();
            if (project == null) return Ok(false);
            return Ok(true);
        }

        [HttpGet("projects")]
        public async Task<IActionResult> Get()
        {
            var res = _client.Projects.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpGet("projects/count")]
        public async Task<IActionResult> GetCount()
        {
            var count = _client.Projects.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var res = _client.Projects.Find(x => x.Id == id).FirstOrDefault();
            return Ok(res);
        }

        [HttpGet("projects/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(int pageNo, int perPage)
        {
            if (pageNo < 1) pageNo = 1;
            var res = _client.Projects.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpDelete("projects")]
        public async Task<IActionResult> Clear()
        {
            var res = _client.Projects.DeleteMany(x => true);
            return Ok(res);
        }

        [HttpDelete("projects/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var res = _client.Projects.DeleteOne(x => x.Id == id);
            return Ok(res);
        }        
    }
}
