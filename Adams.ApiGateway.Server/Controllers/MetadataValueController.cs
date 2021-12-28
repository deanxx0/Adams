﻿using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using NAVIAIServices.Entities;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class MetadataValueController : ControllerBase
    {
        DbClient _client;
        public MetadataValueController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/metadatavalues")]
        public async Task<IActionResult> Add(string projectId, [FromBody] MetadataValue entity)
        {
            var newMetadataValue = new MetadataValue(entity.ItemId, entity.KeyId, entity.Type, entity.Value, true);
            if (entity.Id != null)
                newMetadataValue.SetId(entity.Id);

            var metadataValues = _client.GetProjectDB(projectId).MetadataValues();
            metadataValues.InsertOne(newMetadataValue);
            return Ok(newMetadataValue);
        }

        [HttpGet("projects/{projectId}/metadatavalues/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            var metadataValues = _client.GetProjectDB(projectId).MetadataValues();
            var count = metadataValues.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/metadatavalues/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            if (pageNo < 1) pageNo = 1;
            var metadataValues = _client.GetProjectDB(projectId).MetadataValues();
            var res = metadataValues.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/metadatavalues")]
        public async Task<IActionResult> Get(string projectId)
        {
            var metadataValues = _client.GetProjectDB(projectId).MetadataValues();
            var res = metadataValues.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/metadatavalues")]
        public async Task<IActionResult> Update(string projectId, [FromBody] MetadataValue entity)
        {
            var metadataValues = _client.GetProjectDB(projectId).MetadataValues();
            metadataValues.ReplaceOne(x => x.Id == entity.Id, entity);
            return Ok(entity);
        }
    }
}
