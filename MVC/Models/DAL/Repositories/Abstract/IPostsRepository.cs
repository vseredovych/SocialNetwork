using MVC.Models.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Models.DAL.Repositories
{
    public interface IPostsRepository
    {
        Task<IList<Post>> GetAll();
        Task<IList<Post>> GetByAuthor(string author);
        Task<int> GetLikesById(string id);
        Task<Post> IncPostLikes(string id);
        Task<Post> DicPostLikes(string id);
    }
}
