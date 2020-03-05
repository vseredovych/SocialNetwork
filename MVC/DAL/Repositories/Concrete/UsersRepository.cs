using MongoDB.Driver;
using MVC.DAL.DatabaseConfig;
using MVC.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        IMongoCollection<User> collection;

        public UsersRepository(IMongoContext context)
        {
            this.collection = context.Users();
        }
        public async Task<IList<User>> GetAllAsync()
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Empty;

            var result = await collection.Find(filter).ToListAsync();

            return result;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(el => el.Email, email);

            var result = await collection
                .Find(filter)
                .SingleAsync();

            return result;
        }
    }
}