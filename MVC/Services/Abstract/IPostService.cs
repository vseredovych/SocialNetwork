using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Models.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostItemViewModel>> GetAllAsync();
        Task<int> GetLikesAsync(string id);
        Task<PostItemViewModel> LikePostAsync(string id);
        Task<PostItemViewModel> DislikePostAsync(string id);
    }
}
