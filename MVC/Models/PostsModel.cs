using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
