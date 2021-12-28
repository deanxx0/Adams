﻿using Adams.ApiGateway.Server.Db;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NAVIAIServices.Entities;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class ClassInfoController : ControllerBase
    {
        DbClient _client;
        public ClassInfoController(DbClient client)
        {
            _client = client;
        }

        [HttpPost("projects/{projectId}/classinfos")]
        public async Task<IActionResult> Add(string projectId, [FromBody] ClassInfo classInfo)
        {
            var isEnabled = classInfo.IsEnabled == null ? true : classInfo.IsEnabled.Value;
            var newClassInfo = classInfo.Id == null ?
                new ClassInfo(classInfo.Name, classInfo.Description, classInfo.Key, classInfo.R, classInfo.G, classInfo.B, isEnabled) :
                new ClassInfo(classInfo.Id, classInfo.Name, classInfo.Description, classInfo.Key, classInfo.R, classInfo.G, classInfo.B, isEnabled);

            var classInfos = _client.GetProjectDB(projectId).ClassInfos();
            classInfos.InsertOne(newClassInfo);
            return Ok(newClassInfo);
        }

        [HttpGet("projects/{projectId}/classinfos/count")]
        public async Task<IActionResult> Count(string projectId)
        {
            var classInfos = _client.GetProjectDB(projectId).ClassInfos();
            var count = classInfos.CountDocuments(x => true);
            return Ok(count);
        }

        [HttpGet("projects/{projectId}/classinfos/{pageNo}/{perPage}")]
        public async Task<IActionResult> Get(string projectId, int pageNo, int perPage)
        {
            if (pageNo < 1) pageNo = 1;
            var classInfos = _client.GetProjectDB(projectId).ClassInfos();
            var res = classInfos.Find(x => true).Skip((pageNo - 1) * perPage).Limit(perPage).ToList();
            if (res.Count == 0) return BadRequest($"No Content on the pageNo {pageNo}");
            return Ok(res);
        }

        [HttpGet("projects/{projectId}/classinfos")]
        public async Task<IActionResult> Get(string projectId)
        {
            var classInfos = _client.GetProjectDB(projectId).ClassInfos();
            var res = classInfos.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpPut("projects/{projectId}/classinfos")]
        public async Task<IActionResult> Update(string projectId, [FromBody] ClassInfo classInfoIn)
        {
            var classInfos = _client.GetProjectDB(projectId).ClassInfos();
            classInfos.ReplaceOne(x => x.Id == classInfoIn.Id, classInfoIn);
            return Ok(classInfoIn);
        }
    }
}