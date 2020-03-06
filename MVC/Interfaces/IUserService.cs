using MVC.Core.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel> GetByEmailAsync(string email);
        Task<bool> IsUserExistsAsync(string email);
        Task<bool> CheckPasswordByEmailAsync(string email, string password);
        void InsertUserAsync(UserViewModel userModel);
        //Task<IEnumerable<User>> GetAllAsync();
        //Task<User> GetByEmailAsync(string email);
        //Task<bool> IsUserExistsAsync(string email);
        //Task<bool> CheckPasswordByEmailAsync(string email, string password);
    }
}
