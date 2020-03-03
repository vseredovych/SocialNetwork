namespace MVC.Models.DAL.DatabaseConfig
{
    public interface IMongoConfiguration
    {
        string connectionString { get; set; }
        string databaseName { get; set; }
    }
}
