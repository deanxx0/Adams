using Adams.ApiGateway.Server.Models;
using MongoDB.Driver;

namespace Adams.ApiGateway.Server.Db
{
    public class DbCollection
    {
        public IMongoCollection<User> users;
        public DbCollection()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("adams");
            users = db.GetCollection<User>("users");
        }
    }
}
