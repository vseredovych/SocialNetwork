namespace MVC.Core.Database.Config
{
    public class Neo4jConfiguration : INeo4jConfiguration 
    {
        public string connectionString { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
