using Adams.ApiGateway.Server.Models;
using MongoDB.Driver;
using NAVIAIServices.Entities;

namespace Adams.ApiGateway.Server.Db
{
    public class DbCollection
    {
        public IMongoCollection<User> users;
        public IMongoCollection<Project> projects;
        public DbCollection(IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("mongo");
            var client = new MongoClient(connStr);
            var db = client.GetDatabase("adams");
            users = db.GetCollection<User>("users");
            projects = db.GetCollection<Project>("projects");
        }
    }
}
