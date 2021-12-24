using Adams.ApiGateway.Server.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Adams.ApiGateway.Server.Controllers
{
    [ApiController]
    [Authorize(Policy = PolicyNames.AdminOnly)]
    public class AuthTestController : ControllerBase
    {
        [HttpGet("authtest")]
        public async Task<IActionResult> Get()
        {
            return Ok("Auth OK!");
        }
    }
}
