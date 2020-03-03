using MVC.Models.DAL.Entities;
using System.Collections.Generic;

namespace MVC.Models.DAL.Repositories
{
    public interface IPostsRepository
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetByAuthor(string author);

        Post IncPostLikes(string id);

        Post DicPostLikes(string id);
    }
}
