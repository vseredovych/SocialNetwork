using MVC.DAL.Repositories;

namespace MVC.DAL
{
    public interface IUnitOfWork
    {
        IPostsRepository PostsRepository { get; }
    }
}
