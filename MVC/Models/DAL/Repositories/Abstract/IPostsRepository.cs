using MVC.Models.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Models.DAL.Repositories
{
    public interface IPostsRepository
    {
        Task<IList<Post>> GetAllAsync();
        Task<IList<Post>> GetByAuthorAsync(string author);
        Task<int> GetLikesByIdAsync(string id);
        Task<Post> IncPostLikesAsync(string id);
        Task<Post> DicPostLikesAsync(string id);
    }
}
