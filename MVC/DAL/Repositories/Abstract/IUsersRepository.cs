using MVC.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.DAL.Repositories
{
    public interface IUsersRepository
    {
        Task<IList<User>> GetAllAsync();
        Task<User> GetByEmailAsync(string email);
    }
}
