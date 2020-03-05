using AutoMapper;
using MongoDB.Driver;
using MVC.Core.Database.Config;
using MVC.Core.Entities;
using MVC.ViewModels;
using MVC.Web.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Web.Services
{
    public class UserService : IUserService
    {

        private readonly IMongoContext _context;
        private readonly IMapper _mapper;

        public UserService(IMongoContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var result = await _context.Users.AsQueryable().ToListAsync();
            return result.Select(p => _mapper.Map<UserViewModel>(p));
        }
        public async Task<UserViewModel> GetByEmailAsync(string email)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(el => el.Email, email);

            var result = await _context
                .Users
                .Find(filter)
                .SingleAsync();

            return _mapper.Map<UserViewModel>(result);
        }

        public async Task<bool> IsUserExistsAsync(string email)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(el => el.Email, email);

            var user = await _context.Users.Find(filter).SingleAsync();
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
    }
}


//    public class UserService : IUserService
//    {
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;

//        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
//        {
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }
//        public async Task<IEnumerable<User>> GetAllAsync()
//        {
//            List<User> users = new List<User>();

//            //foreach (var user in await _unitOfWork.UsersRepository.GetAllAsync())
//            //{
//            //    var dto = _mapper.Map<User, User>(user);
//            //    users.Add(dto);
//            //}
//            return users;
//        }
//        public async Task<User> GetByEmailAsync(string email)
//        {
//            List<User> users = new List<User>();

//            var user = await _unitOfWork.UsersRepository.GetByEmailAsync(email);
//            var dto = _mapper.Map<User, User>(user);

//            return dto;
//        }

//        
//        public async Task<bool> CheckPasswordByEmailAsync(string email, string password)
//        {
//            List<User> users = new List<User>();

//            var user = await _unitOfWork.UsersRepository.GetByEmailAsync(email);
//            if (user.HashPassword == password)
//            {
//                return true;
//            }
//            return false;
//        }

//    }
