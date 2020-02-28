using DAL.Entities;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public interface IPostsRepository
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetByAuthor(string author);
    }
}
