namespace Adams.ApiGateway.Server.Models
{
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserClaim { get; set; }
    }
}
