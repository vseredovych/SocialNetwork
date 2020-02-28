using DAL;
using DAL.Entities;
using System.Collections.Generic;

namespace MVC.Models
{
    public class PostsModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostsModel(IUnitOfWork uniteOfWork)
        {
            _unitOfWork = uniteOfWork;
        }
        public IEnumerable<Post> Get()
        {
            var posts = _unitOfWork.PostsRepository.GetAll();
            return posts;
            //foreach (var el in posts)
            //{
            //    el.author
            //}
        }
    }
}
