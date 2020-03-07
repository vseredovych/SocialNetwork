using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using MVC.Core.Database.Config;
using MVC.Core.Entities;
using MVC.Interfaces;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class PostService : IPostService
    {
        private readonly IMongoContext _context;
        private readonly IMapper _mapper;

        public PostService(IMongoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostViewModel>> GetAllAsync()
        {
            var result = await _context.Posts.AsQueryable().ToListAsync();

            return result.Select(p => _mapper.Map<PostViewModel>(p));
        }
        public async Task<IEnumerable<PostViewModel>> GetByAuthorAsync(string author)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.AuthorEmail, author);

            var result = await _context
                .Posts
                .AsQueryable()
                .ToListAsync();

            return result.Select(p => _mapper.Map<PostViewModel>(p));
        }

        public async Task<int> GetLikesByIdAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.Id, id);

            var result = await _context
                .Posts
                .Find(filter)
                .Limit(1)
                .SingleAsync();

            return result.Likes;
        }
        public async void InsertPostAsync(Post post)
        {
            var builder = Builders<Post>.Filter;
            post.Id = Convert.ToString(ObjectId.GenerateNewId());
            await _context.Posts.InsertOneAsync(post);
        }
        public async void InsertCommentAsync(Post post)
        {
            var builder = Builders<Post>.Filter;
            await _context.Posts.InsertOneAsync(post);
        }
        public async Task<PostViewModel> IncPostLikesAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.Id, id);

            var update = new UpdateDefinitionBuilder<Post>().Inc(el => el.Likes, 1);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;
            options.Projection = new ProjectionDefinitionBuilder<Post>().Include(el => el.Likes);
            var result = await _context.Posts.FindOneAndUpdateAsync<Post>(el => el.Id == id, update, options);

            return _mapper.Map<PostViewModel>(result);
        }
        public async Task<PostViewModel> DicPostLikesAsync(string id)
        {
            var builder = Builders<Post>.Filter;
            var filter = builder.Eq(el => el.Id, id);

            var update = new UpdateDefinitionBuilder<Post>().Inc(el => el.Likes, -1);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;
            options.Projection = new ProjectionDefinitionBuilder<Post>().Include(el => el.Likes);
            var result = await _context.Posts.FindOneAndUpdateAsync<Post>(el => el.Id == id, update, options);

            return _mapper.Map<PostViewModel>(result);
        }

        public async void InsertCommentAsync(Comment comment, string postId)
        {
            var filter = Builders<Post>.Filter.Eq(el => el.Id, postId);
            var update = Builders<Post>.Update
                    .Push<Comment>(el => el.Comments, comment);

            await _context.Posts.FindOneAndUpdateAsync(filter, update);
        }
    }
}
