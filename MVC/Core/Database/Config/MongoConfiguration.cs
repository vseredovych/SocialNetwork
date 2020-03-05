namespace MVC.Core.Database.Config
{
    public class MongoConfiguration : IMongoConfiguration
    {
        public string connectionString { get; set; }
        public string databaseName { get; set; }
    }
}
