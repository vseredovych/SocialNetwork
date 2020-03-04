using MongoDB.Driver;
using MVC.Models.DAL.DatabaseConfig;
using MVC.Models.DAL.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MVC.Models.DAL.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        IMongoCollection<Post> collection;

        public PostsRepository(IMongoContext context)
        {
            this.collection = context.Posts();
        }
        public async Task<IList<Post>> GetAllAsync()
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Empty;

            var result = await collection.Find<Post>(filter).ToListAsync<Post>();
       
            return result;
        }
        public async Task<IList<Post>> GetByAuthorAsync(string author)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.AuthorEmail, author);

            var result = await collection
                .Find(filter)
                .ToListAsync();

            return result;
        }

        public async Task<int> GetLikesByIdAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el._id, id);

            var result = await collection
                .Find(filter)
                .Limit(1)
                .SingleAsync();

            return result.Likes;
        }
        public async Task<Post> IncPostLikesAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el._id, id);

            var update = new UpdateDefinitionBuilder<Post>().Inc(el => el.Likes, 1);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;
            options.Projection = new ProjectionDefinitionBuilder<Post>().Include(el => el.Likes);
            var result = await collection.FindOneAndUpdateAsync<Post>(el => el._id == id, update, options);
            
            return result;
        }
        public async Task<Post> DicPostLikesAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el._id, id);

            var update = new UpdateDefinitionBuilder<Post>().Inc(el => el.Likes, -1);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;
            options.Projection = new ProjectionDefinitionBuilder<Post>().Include(el => el.Likes);
            var result = await collection.FindOneAndUpdateAsync<Post>(el => el._id == id, update, options);

            return result;
        }
    }
}
