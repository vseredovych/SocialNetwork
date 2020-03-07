using MVC.Core.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> GetAllAsync();
        Task<IEnumerable<PostViewModel>> GetByAuthorAsync(string author);
        Task<int> GetLikesByIdAsync(string id);
        Task<PostViewModel> IncPostLikesAsync(string id);
        Task<PostViewModel> DicPostLikesAsync(string id);
        void InsertPostAsync(Post post);
        void InsertCommentAsync(Comment comment, string postId);
    }
}
