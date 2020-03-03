using MongoDB.Driver;
using MVC.Models.DAL.DatabaseConfig;
using MVC.Models.DAL.Entities;
using System.Collections.Generic;

namespace MVC.Models.DAL.Repositories
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
            var filter = builder.Eq(el => el.AuthorEmail, author);

            var result = collection
                .Find(filter)
                .ToEnumerable();

            return result;
        }
        public Post IncPostLikes(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el._id, id);

            var update = new UpdateDefinitionBuilder<Post>().Inc(el => el.Likes, 1);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;
            options.Projection = new ProjectionDefinitionBuilder<Post>().Include(el => el.Likes);
            var result = collection.FindOneAndUpdate<Post>(el => el._id == id, update, options);
            
            return result;
        }
        public Post DicPostLikes(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el._id, id);

            var update = new UpdateDefinitionBuilder<Post>().Inc(el => el.Likes, -1);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;
            options.Projection = new ProjectionDefinitionBuilder<Post>().Include(el => el.Likes);
            var result = collection.FindOneAndUpdate<Post>(el => el._id == id, update, options);

            return result;
        }
    }
}
