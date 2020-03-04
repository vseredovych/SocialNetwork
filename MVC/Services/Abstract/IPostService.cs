using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Models.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAllAsync();
        Task<int> GetLikesAsync(string id);
        Task<PostViewModel> LikePostAsync(string id);
        Task<PostViewModel> DislikePostAsync(string id);
    }
}
