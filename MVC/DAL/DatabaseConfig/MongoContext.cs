using MongoDB.Driver;
using MVC.DAL.Entities;

namespace MVC.DAL.DatabaseConfig
{
    public class MongoContext : IMongoContext
    {
        IMongoDatabase database;

        public MongoContext(IMongoConfiguration mongoConfig)
        {
            var client = new MongoClient(mongoConfig.connectionString);
            database = client.GetDatabase(mongoConfig.databaseName);
        }

        public IMongoCollection<Post> Posts()
        {
            return database.GetCollection<Post>("posts");
        }
        public IMongoCollection<User> Users()
        {
            return database.GetCollection<User>("users");
        }
    }
}
