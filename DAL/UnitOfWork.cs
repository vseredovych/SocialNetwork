using DAL.DatabaseConfig;
using DAL.Repositories;
using Unity.Resolution;

namespace DAL
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
