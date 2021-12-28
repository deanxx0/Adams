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
        public static IMongoCollection<Item> Items(this IMongoDatabase db)
        {
            return db.GetCollection<Item>("items");
        }
        public static IMongoCollection<Augmentation> Augmentations(this IMongoDatabase db)
        {
            return db.GetCollection<Augmentation>("augmentations");
        }
        public static IMongoCollection<TrainConfiguration> TrainConfiguraitons(this IMongoDatabase db)
        {
            return db.GetCollection<TrainConfiguration>("trainConfigurations");
        }
        public static IMongoCollection<ImageInfo> ImageInfos(this IMongoDatabase db)
        {
            return db.GetCollection<ImageInfo>("imageInfos");
        }
        public static IMongoCollection<MetadataValue> MetadataValues(this IMongoDatabase db)
        {
            return db.GetCollection<MetadataValue>("metadataValues");
        }
        public static IMongoCollection<Label> Labels(this IMongoDatabase db)
        {
            return db.GetCollection<Label>("labels");
        }
        public static IMongoCollection<Train> Trains(this IMongoDatabase db)
        {
            return db.GetCollection<Train>("trains");
        }
        public static IMongoCollection<Dataset> Datasets(this IMongoDatabase db)
        {
            return db.GetCollection<Dataset>("datasets");
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
