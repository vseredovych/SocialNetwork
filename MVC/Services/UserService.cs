using AutoMapper;
using MongoDB.Driver;
using MVC.Core.Database.Config;
using MVC.Core.Entities;
using MVC.ViewModels;
using MVC.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using MongoDB.Bson;

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

            if (userModel.Password == null)
            {
                user.HashPassword = oldUser.HashPassword;
            }

            var builder = Builders<User>.Filter;
            var filter = builder.Eq(el => el.Email, userModel.Email);
            var options = new FindOneAndUpdateOptions<Post>();
            options.ReturnDocument = ReturnDocument.After;

            await _context.Users.ReplaceOneAsync(filter, user);
            return userModel;
        }

        public async Task<ProfileViewModel> GetProfileModel(UserViewModel userModel)
        {
            var user = await GetByEmailAsync(userModel.Email);
            var updateModel = _mapper.Map<ProfileViewModel>(userModel);
            return updateModel;
        }

        //public async Task<IEnumerable<UserViewModel>> GetFriendUsersByIdAsync(string userId)
        //{
        //    var builder = Builders<User>.Filter;
        //    var filter = builder.Eq(el => el.Id, userId);

        //    var result = await _context.Users.Find<User>(filter).SingleAsync();

        //    var friends = new List<UserViewModel>();
        //    foreach (var id in result.Friends)
        //    {
        //        friends.Add(await GetByEmailAsync(id.Email));
        //    }

        //    return friends;
        //}
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
    }
}