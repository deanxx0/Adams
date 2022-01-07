using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class AugmentationController : ControllerBase
    {
        DbClient _client;
        public AugmentationController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/augmentations")]
        public async Task<IActionResult> Add(string projectId, [FromBody] Augmentation entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var description = entity.Description ?? "";
            var newAugmentation = new Augmentation(
                entity.Name,
                description,
                entity.Mirror,
                entity.Flip,
                entity.Rotation90,
                entity.Zoom,
                entity.Shift,
                entity.Tilt,
                entity.Rotation,
                entity.BorderMode,
                entity.Contrast,
                entity.Brightness,
                entity.Shade,
                entity.Hue,
                entity.Saturation,
                entity.Noise,
                entity.Smoothing,
                entity.ColorNoise,
                entity.PartialFocus,
                entity.Probability,
                entity.RandomCount
                );
            if (entity.Id != null)
                newAugmentation.SetId(entity.Id);

            var augmentations = _client.GetProjectDB(projectId).Augmentations();
            augmentations.InsertOne(newAugmentation);
            return Ok(newAugmentation);
        }

        [HttpGet("projects/{projectId}/augmentations/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var augmentations = _client.GetProjectDB(projectId).Augmentations();
            var count = augmentations.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/augmentations/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            if (pageNo < 1) pageNo = 1;
            var augmentations = _client.GetProjectDB(projectId).Augmentations();
            var res = augmentations.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/augmentations")]
        public async Task<IActionResult> Get(string projectId)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var augmentations = _client.GetProjectDB(projectId).Augmentations();
            var res = augmentations.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/augmentations")]
        public async Task<IActionResult> Update(string projectId, [FromBody] Augmentation entity)
        {
            // project id check
            var project = _client.Projects.Find(x => x.Id == projectId).FirstOrDefault();
            if (project == null) return NotFound("No project id");

            var augmentations = _client.GetProjectDB(projectId).Augmentations();
            augmentations.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
