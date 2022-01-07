using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;
using NAVIAIServices.Enums;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class TrainController : ControllerBase
    {
        DbClient _client;
        public TrainController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/trains")]
        public async Task<IActionResult> Add(string projectId, [FromBody] Train entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var configs = _client.GetProjectDB(projectId).TrainConfiguraitons();
            var config = configs.Find(x => x.Id == entity.ConfigurationId).FirstOrDefault();
            if (config == null) return NotFound();

            var augs = _client.GetProjectDB(projectId).Augmentations();
            var aug = augs.Find(x => x.Id == entity.AugmentationId).FirstOrDefault();
            if (aug == null) return NotFound();

            var datasets = _client.GetProjectDB(projectId).Datasets();

            var trainset = datasets.Find(x => entity.TrainSetIdList.Contains(x.Id)).ToList();
            foreach(var t in trainset)
            {
                if (t.Type != DatasetTypes.Training) return BadRequest();
            }
            
            var validationset = datasets.Find(x => entity.ValidationSetIdList.Contains(x.Id)).ToList();
            foreach(var v in validationset)
            {
                if (v.Type != DatasetTypes.Validation) return BadRequest();
            }

            var description = entity.Description ?? "";
            var newTrain = new Train(entity.Name, description, entity.ConfigurationId, entity.AugmentationId, entity.TrainSetIdList.ToList(), entity.ValidationSetIdList.ToList(), true);
            if (entity.Id != null)
                newTrain.SetId(entity.Id);

            var trains = _client.GetProjectDB(projectId).Trains();
            trains.InsertOne(newTrain);
            return Ok(newTrain);
        }

        [HttpGet("projects/{projectId}/trains/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var trains = _client.GetProjectDB(projectId).Trains();
            var count = trains.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/trains/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            if (pageNo < 1) pageNo = 1;
            var trains = _client.GetProjectDB(projectId).Trains();
            var res = trains.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/trains")]
        public async Task<IActionResult> Get(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var trains = _client.GetProjectDB(projectId).Trains();
            var res = trains.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/trains")]
        public async Task<IActionResult> Update(string projectId, [FromBody] Train entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var trains = _client.GetProjectDB(projectId).Trains();
            trains.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
