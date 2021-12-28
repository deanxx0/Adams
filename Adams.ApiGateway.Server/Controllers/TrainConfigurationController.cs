using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class TrainConfigurationController : ControllerBase
    {
        DbClient _client;
        public TrainConfigurationController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/trainconfigurations")]
        public async Task<IActionResult> Add(string projectId, [FromBody] TrainConfiguration entity)
        {
            var description = entity.Description ?? "";
            var newTrainConfiguration = new TrainConfiguration(
                entity.Name,
                description,
                entity.Width,
                entity.Height,
                entity.BatchSize,
                entity.PretrainModelPath,
                entity.MaxIteration,
                entity.StepCount,
                entity.BaseLearningRate,
                entity.Gamma,
                entity.UseCacheMemory,
                entity.GPUIndex,
                entity.SaveBestPosition,
                entity.SavingPercentage
                );
            if (entity.Id != null)
                newTrainConfiguration.SetId(entity.Id);

            var trainConfigurations = _client.GetProjectDB(projectId).TrainConfiguraitons();
            trainConfigurations.InsertOne(newTrainConfiguration);
            return Ok(newTrainConfiguration);
        }

        [HttpGet("projects/{projectId}/trainconfigurations/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            var trainConfigurations = _client.GetProjectDB(projectId).TrainConfiguraitons();
            var count = trainConfigurations.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/trainconfigurations/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            if (pageNo < 1) pageNo = 1;
            var trainConfigurations = _client.GetProjectDB(projectId).TrainConfiguraitons();
            var res = trainConfigurations.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/trainconfigurations")]
        public async Task<IActionResult> Get(string projectId)
        {
            var trainConfigurations = _client.GetProjectDB(projectId).TrainConfiguraitons();
            var res = trainConfigurations.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/trainconfigurations")]
        public async Task<IActionResult> Update(string projectId, [FromBody] TrainConfiguration entity)
        {
            var trainConfigurations = _client.GetProjectDB(projectId).TrainConfiguraitons();
            trainConfigurations.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
