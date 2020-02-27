using DAL.DatabaseConfig;
using DAL.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        IMongoCollection<Post> collection;

        public PostsRepository(IMongoContext context)
        {
            this.collection = context.Posts();
        }
        public IEnumerable<Post> GetAll()
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Empty;

            var result = collection
                .Find(filter)
                .Limit(5)
                .ToEnumerable();

            return result;
        }
        public IEnumerable<Post> GetByAuthor(string author)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.Author, author);

            var result = collection
                .Find(filter)
                .ToEnumerable();

            return result;
        }
    }
}
