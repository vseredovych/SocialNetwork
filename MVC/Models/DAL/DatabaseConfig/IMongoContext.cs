

using MongoDB.Driver;
using MVC.Models.DAL.Entities;

namespace MVC.Models.DAL.DatabaseConfig
{
    public interface IMongoContext
    {
        IMongoCollection<Post> Posts();
    }
}
