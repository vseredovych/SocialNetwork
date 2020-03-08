using MVC.Core.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAllAsync();
        Task<PostViewModel> GetByIdAsync(string author);
        Task<IEnumerable<PostViewModel>> GetByAuthorAsync(string author);
        Task<int> GetLikesByIdAsync(string id);
        Task<PostViewModel> IncViewsAsync(string id);
        void LikeClicked(string userEmail, string postId);
        void AddNewPostAsync(string creatorEmail, string postText);
        void AddNewCommentAsync(string creatorEmail, string commentText, string postId);
    }
}
