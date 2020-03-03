using MVC.Models.BLL.DTOs;
using MVC.Models.DAL.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Models.BLL.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostItemViewModel>> GetAll();
        Task<int> GetLikes(string id);
        Task<PostItemViewModel> LikePost(string id);
        Task<PostItemViewModel> DislikePost(string id);
    }
}
