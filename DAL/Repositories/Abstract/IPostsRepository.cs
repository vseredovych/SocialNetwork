using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public interface IPostsRepository
    {
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetByAuthor(string author);
    }
}
