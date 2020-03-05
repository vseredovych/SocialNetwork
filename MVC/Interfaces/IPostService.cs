using MVC.Core.Entities;
using MVC.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Web.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAllAsync();
        Task<IEnumerable<PostViewModel>> GetByAuthorAsync(string author);
        Task<int> GetLikesByIdAsync(string id);
        Task<PostViewModel> IncPostLikesAsync(string id);
        Task<PostViewModel> DicPostLikesAsync(string id);
        void InsertPostAsync(Post post);

        //Task<IEnumerable<PostViewModel>> GetAllAsync();
        //Task<int> GetLikesAsync(string id);
        //Task<PostViewModel> LikePostAsync(string id);
        //Task<PostViewModel> DislikePostAsync(string id);
        //public void InsertPost(PostViewModel postModel);
    }
}
