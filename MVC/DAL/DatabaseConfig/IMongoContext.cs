using MongoDB.Driver;
using MVC.DAL.Entities;

namespace MVC.DAL.DatabaseConfig
{
    public interface IMongoContext
    {
        IMongoCollection<Post> Posts();
        IMongoCollection<User> Users();
    }
}
