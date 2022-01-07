using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class InputChannelController : ControllerBase
    {
        DbClient _client;
        public InputChannelController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/inputchannels")]
        public async Task<IActionResult> Add(string projectId, [FromBody] InputChannel entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var description = entity.Description ?? "";
            var namingRegex = entity.NamingRegex ?? "";
            var newInputChannel = new InputChannel(entity.Name, entity.IsColor, description, namingRegex, true);
            if (entity.Id != null)
                newInputChannel.SetId(entity.Id);
            var inputChannels = _client.GetProjectDB(projectId).InputChannels();
            inputChannels.InsertOne(newInputChannel);
            return Ok(newInputChannel);
        }

        [HttpGet("projects/{projectId}/inputchannels/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var inputChannels = _client.GetProjectDB(projectId).InputChannels();
            var count = inputChannels.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/inputchannels/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            if (pageNo < 1) pageNo = 1;
            var inputChannels = _client.GetProjectDB(projectId).InputChannels();
            var res = inputChannels.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/inputchannels")]
        public async Task<IActionResult> Get(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var inputChannels = _client.GetProjectDB(projectId).InputChannels();
            var res = inputChannels.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/inputchannels")]
        public async Task<IActionResult> Update(string projectId, [FromBody] InputChannel entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var inputChannels = _client.GetProjectDB(projectId).InputChannels();
            inputChannels.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
