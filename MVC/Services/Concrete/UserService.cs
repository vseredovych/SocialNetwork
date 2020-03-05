using AutoMapper;
using MVC.DAL;
using MVC.DAL.Entities;
using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            List<UserModel> users = new List<UserModel>();

            foreach (var user in await _unitOfWork.UsersRepository.GetAllAsync())
            {
                var dto = _mapper.Map<User, UserModel>(user);
                users.Add(dto);
            }
            return users;
        }
        public async Task<UserModel> GetByEmailAsync(string email)
        {
            List<UserModel> users = new List<UserModel>();

            var user = await _unitOfWork.UsersRepository.GetByEmailAsync(email);
            var dto = _mapper.Map<User, UserModel>(user);

            return dto;
        }

        public async Task<bool> IsUserExistsAsync(string email)
        {
            List<UserModel> users = new List<UserModel>();

            var user = await _unitOfWork.UsersRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheckPasswordByEmailAsync(string email, string password)
        {
            List<UserModel> users = new List<UserModel>();

            var user = await _unitOfWork.UsersRepository.GetByEmailAsync(email);
            if (user.HashPassword == password)
            {
                return true;
            }
            return false;
        }

    }
}
