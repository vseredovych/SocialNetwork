
using AutoMapper;
using MVC.DAL;
using MVC.DAL.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostViewModel>> GetAllAsync()
        {
            List<PostViewModel> posts = new List<PostViewModel>();

            foreach (var post in await _unitOfWork.PostsRepository.GetAllAsync())
            {
                PostViewModel dto = _mapper.Map<Post, PostViewModel>(post);
                posts.Add(dto);
            }
            return posts;
        }
        public async Task<int> GetLikesAsync(string id)
        {

            var likes = await _unitOfWork.PostsRepository.GetLikesByIdAsync(id);
            return likes;
        }
        public async Task<PostViewModel> LikePostAsync(string id)
        {
            var post = await _unitOfWork.PostsRepository.IncPostLikesAsync(id);
            PostViewModel dto = _mapper.Map<Post, PostViewModel>(post);

            return dto;
        }
        public async Task<PostViewModel> DislikePostAsync(string id)
        {
            var post = await _unitOfWork.PostsRepository.DicPostLikesAsync(id);
            PostViewModel dto = _mapper.Map<Post, PostViewModel>(post);

            return dto;
        }
    }
}
