namespace MVC.Core.Database.Config
{
    public interface IMongoConfiguration
    {
        string connectionString { get; set; }
        string databaseName { get; set; }
    }
}
