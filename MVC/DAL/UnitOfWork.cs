using MVC.DAL.DatabaseConfig;
using MVC.DAL.Repositories;
using Unity.Resolution;

namespace MVC.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        private IPostsRepository _postsRepository;

        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public IPostsRepository PostsRepository
        {
            get
            {
                if (_postsRepository == null)
                {
                    _postsRepository = DependencyInjectorDAL.
                        Resolve<IPostsRepository>(new ParameterOverride("context", _context));
                }

                return _postsRepository;
            }
        }
    }
}
