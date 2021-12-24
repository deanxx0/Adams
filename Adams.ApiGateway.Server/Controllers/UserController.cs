using Adams.ApiGateway.Server.Auth;
using Adams.ApiGateway.Server.Db;
using Adams.ApiGateway.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        IMongoCollection<User> _users;
        public UserController(DbCollection dbCollection)
        {
            _users = dbCollection.users;
        }

        [HttpPost("users")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userCreateDto)
        {
            if (
                userCreateDto.UserClaim != ClaimNames.Admin &&
                userCreateDto.UserClaim != ClaimNames.Member
                )
            {
                return BadRequest($"User Claim should be {ClaimNames.Member} or {ClaimNames.Admin}");
            }

            var hasher = new PasswordHasher<string>();
            var hashedStr = hasher.HashPassword(userCreateDto.UserName, userCreateDto.Password);

            var newUser = new User
            {
                Id = ObjectId.GenerateNewId().ToString(),
                UserName = userCreateDto.UserName,
                Password = hashedStr,
                UserClaim = userCreateDto.UserClaim,
            };

            _users.InsertOne(newUser);
            return Ok(newUser);
        }

        [HttpGet("users")]
        public async Task<IActionResult> Get()
        {
            var res = _users.Find(x => true).ToList();
            return Ok(res);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var res = _users.Find(x => x.Id == id).FirstOrDefault();
            return Ok(res);
        }

        [HttpPut("users")]
        public async Task<IActionResult> Update([FromBody]User user)
        {
            _users.ReplaceOne(x => x.Id == user.Id, user);
            return Ok(user);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _users.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
