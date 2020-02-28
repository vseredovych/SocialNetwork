using DAL.Entities;
using MongoDB.Driver;

namespace DAL.DatabaseConfig
{
    public interface IMongoContext
    {
        IMongoCollection<Post> Posts();
    }
}
