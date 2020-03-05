using MongoDB.Driver;
using MVC.Core.Entities;

namespace MVC.Core.Database.Config
{
    public interface IMongoContext
    {
        IMongoCollection<Post> Posts { get; }
        IMongoCollection<User> Users { get; }
    }
}
