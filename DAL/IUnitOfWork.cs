using DAL.Repositories;

namespace DAL
{
    public interface IUnitOfWork
    {
        IPostsRepository PostsRepository { get; }
    }
}
