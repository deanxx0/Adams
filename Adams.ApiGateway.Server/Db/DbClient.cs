using Adams.ApiGateway.Server.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using NAVIAIServices.Entities;

namespace Adams.ApiGateway.Server.Db
{
    internal static class Extensions
    {
        public static IMongoCollection<ClassInfo> ClassInfos(this IMongoDatabase db)
        {
            return db.GetCollection<ClassInfo>("classInfos");
        }
        public static IMongoCollection<Project> Projects(this IMongoDatabase db)
        {
            return db.GetCollection<Project>("projects");
        }
        public static IMongoCollection<MetadataKey> MetadataKeys(this IMongoDatabase db)
        {
            return db.GetCollection<MetadataKey>("metadataKeys");
        }
        public static IMongoCollection<InputChannel> InputChannels(this IMongoDatabase db)
        {
            return db.GetCollection<InputChannel>("inputChannels");
        }
    }
    public class DbClient
    {
        MongoClient _client;
        IMongoDatabase _db => _client.GetDatabase("adams");
        public IMongoCollection<User> Users => _db.GetCollection<User>("users");
        public IMongoCollection<Project> Projects => _db.GetCollection<Project>("projects");

        public DbClient(IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("mongo");
            _client = new MongoClient(connString);
        }

        public IMongoDatabase GetProjectDB(string projectId)
        {
            var project = Projects.AsQueryable().Where(x => x.Id == projectId).FirstOrDefault();
            if (project == null)
                throw new Exception();

            var projectDB = _client.GetDatabase($"{project.Name}_{project.Id}");
            return projectDB;
        }

        public string NewId()
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
