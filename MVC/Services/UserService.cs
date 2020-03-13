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
    public class UserService : IUserService
    {

        private readonly IMongoContext _context;
        private readonly IMapper _mapper;

        public UserService(IMongoContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var result = await _context.Users.AsQueryable().ToListAsync();
            return result.Select(p => _mapper.Map<UserViewModel>(p));
        }
        public async Task<UserViewModel> GetByEmailAsync(string email)
        {
            var result = await _context
                .Users
                .AsQueryable()
                .ToListAsync();

            var user = _mapper.Map<UserViewModel>(result.SingleOrDefault(u => u.Email == email));
            return user;
        }
        public async Task<UserViewModel> GetByIdAsync(string id)
        {
            var result = await _context
                .Users
                .AsQueryable()
                .ToListAsync();

            var user = _mapper.Map<UserViewModel>(result.SingleOrDefault(u => u.Id == id));
            return user;
        }
        public async void InsertUserAsync(UserViewModel userModel)
        {
            var user = _mapper.Map<User>(userModel);
            user.Id = Convert.ToString(ObjectId.GenerateNewId());
            await _context.Users.InsertOneAsync(user);
        }
        public async Task<bool> IsUserExistsAsync(string email)
        {
            var result = await _context.Users.AsQueryable().ToListAsync();
            var user = result.SingleOrDefault(u => u.Email == email);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckPasswordByEmailAsync(string email, string password)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(el => el.Email, email);

            var user = await _context.Users.Find(filter).SingleAsync();
            if (user.HashPassword == password)
            {
                return true;
            }
            return false;
        }
        public async Task<ProfileViewModel> UpdateUserAsync(ProfileViewModel userModel)
        {
            var oldUser = await GetByEmailAsync(userModel.Email);

            var user = _mapper.Map<User>(userModel);
            user.Id = oldUser.Id;

            user.Friends = oldUser.Friends;

            if (userModel.Password == null)
            {
                user.HashPassword = oldUser.HashPassword;
            }
            if (userModel.ImageSource == null)
            {
                user.ImageSource = oldUser.ImageSource;
            }

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(el => el.Email, userModel.Email);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;

            await _context.Users.ReplaceOneAsync(filter, user);

            UpdateFriendsUserInfo(user);
            UpdatePostsUserInfo(user);
            UpdateCommentsUserInfo(user);

            return userModel;
        }

        public async Task<ProfileViewModel> GetProfileModel(UserViewModel userModel)
        {
            var user = await GetByEmailAsync(userModel.Email);
            var updateModel = _mapper.Map<ProfileViewModel>(userModel);
            return updateModel;
        }

        public void AddFriend(string requesterEmail, string userEmail)
        {
            AddFrienddByEmailAsync(requesterEmail, userEmail);
            AddFrienddByEmailAsync(userEmail, requesterEmail);
        }
        public void RemoveFriend(string requesterEmail, string userEmail)
        {
            RemoveFriendByEmailAsync(requesterEmail, userEmail);
            RemoveFriendByEmailAsync(userEmail, requesterEmail);
        }

        private async void AddFrienddByEmailAsync(string requesterEmail, string userEmail)
        {
            var friend = _mapper.Map<Friend>(await GetByEmailAsync(userEmail));


            var filter = Builders<User>.Filter.Eq(el => el.Email, requesterEmail);
            var update = Builders<User>.Update
                    .Push<Friend>(el => el.Friends, friend);

            await _context.Users.FindOneAndUpdateAsync(filter, update);
        }
        private async void RemoveFriendByEmailAsync(string requesterEmail, string userEmail)
        {
            var friend = _mapper.Map<Friend>(await GetByEmailAsync(userEmail));

            var filter = Builders<User>.Filter.Eq(el => el.Email, requesterEmail);
            var update = Builders<User>.Update
                    .Pull<Friend>(el => el.Friends, friend);

            await _context.Users.FindOneAndUpdateAsync(filter, update);

        }
        private async void UpdateFriendsUserInfo(User user)
        {
            var update = Builders<User>.Update
                .Set(el => el.Friends[-1].Name, user.Name)
                .Set(el => el.Friends[-1].Surname, user.Surname)
                .Set(el => el.Friends[-1].ImageSource, user.ImageSource);

            await _context.Users.UpdateManyAsync<User>(
                el => el.Friends.Any(el => el.Email == user.Email), update
                );
        }

        private async void UpdatePostsUserInfo(User user)
        {
            var update = Builders<Post>.Update
                .Set(el => el.AuthorName, user.Name)
                .Set(el => el.AuthorSurname, user.Surname)
                .Set(el => el.AuthorImageSource, user.ImageSource);

            await _context.Posts.UpdateManyAsync<Post>(
                el => el.AuthorEmail == user.Email, update
                );
        }
        private async void UpdateCommentsUserInfo(User user)
        {
            //                el => el.Comments.Where(el => el.AuthorEmail == user.Email)
            var update = Builders<Post>.Update
               .Set("comments.$[g].authorName", user.Name)
               .Set("comments.$[g].authorSurname", user.Surname)
               .Set("comments.$[g].authorImageSource", user.ImageSource);

            var filter = Builders<Post>.Filter
                .Where(x => x.Comments.Any(c => c.AuthorEmail == user.Email));

            var updateOprions = new UpdateOptions
            {
                ArrayFilters = new List<ArrayFilterDefinition>
                 {
                    new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("g.authorEmail", user.Email)),
                 }
            };

            await _context.Posts.UpdateManyAsync(filter, update, updateOprions);
        }

    }
}