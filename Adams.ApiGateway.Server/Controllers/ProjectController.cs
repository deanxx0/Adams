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
        IMongoCollection<Project> _projects;
        public ProjectController(DbCollection dbCollection)
        {
            _projects = dbCollection.projects;
        }

        [HttpPost("projects")]
        public async Task<IActionResult> Add([FromBody] Project project)
        {
            var newProject = new Project(project.AIType, project.Name, project.Description);
            if (project.Id != null)
                newProject.SetId(project.Id);

            _projects.InsertOne(newProject);
            return Ok(newProject);
        }

        [HttpGet("projects/contains/{id}")]
        public async Task<IActionResult> Contains(string id)
        {
            var project = _projects.Find(x => x.Id == id).FirstOrDefault();
            if (project == null) return Ok(false);
            return Ok(true);
        }

        [HttpGet("projects")]
        public async Task<IActionResult> Get()
        {
            var res = _projects.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpGet("projects/count")]
        public async Task<IActionResult> GetCount()
        {
            var count = _projects.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var res = _projects.Find(x => x.Id == id).FirstOrDefault();
            return Ok(res);
        }

        [HttpDelete("projects")]
        public async Task<IActionResult> Clear()
        {
            var res = _projects.DeleteMany(x => true);
            return Ok(res);
        }

        [HttpDelete("projects/{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            var res = _projects.DeleteOne(x => x.Id == id);
            return Ok(res);
        }        
    }
}
