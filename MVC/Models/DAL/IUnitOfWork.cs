using MVC.Models.DAL.Repositories;

namespace MVC.Models.DAL
{
    public interface IUnitOfWork
    {
        IPostsRepository PostsRepository { get; }
    }
}
