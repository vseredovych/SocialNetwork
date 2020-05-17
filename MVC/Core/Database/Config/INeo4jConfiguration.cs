namespace MVC.Core.Database.Config
{
    public interface INeo4jConfiguration
    {
        string connectionString { get; set; }
        string username { get; set; }
        string password { get; set; }
    }
}
