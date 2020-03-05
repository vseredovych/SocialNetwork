using MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetByEmailAsync(string email);
        Task<bool> IsUserExistsAsync(string email);
        Task<bool> CheckPasswordByEmailAsync(string email, string password);
    }
}
