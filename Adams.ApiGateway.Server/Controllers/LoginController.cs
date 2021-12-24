using Adams.ApiGateway.Server.Db;
using Adams.ApiGateway.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        IMongoCollection<User> _users;
        string _salt;
        
        public LoginController(DbCollection dbCollection, IConfiguration configuration)
        {
            _users = dbCollection.users;
            _salt = configuration.GetValue<string>("Salt");
        }

        [HttpPost("login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _users.Find(x => x.UserName == username).FirstOrDefault();
            if (user == null) return Unauthorized();

            var hasher = new PasswordHasher<string>();
            var verifyResult = hasher.VerifyHashedPassword(user.UserName, user.Password, password);
            if (verifyResult.Equals(PasswordVerificationResult.Failed)) return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_salt);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(user.UserClaim, "true"),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            HttpContext.Response.Headers.Add("access_token", tokenString);
            return Ok(tokenString);
        }
    }
}
