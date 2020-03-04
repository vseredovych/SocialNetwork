
using AutoMapper;
using MVC.DAL;
using MVC.DAL.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Models.Services
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
        public async Task<IEnumerable<PostItemViewModel>> GetAllAsync()
        {
            List<PostItemViewModel> posts = new List<PostItemViewModel>();

            foreach (var post in await _unitOfWork.PostsRepository.GetAllAsync())
            {
                PostItemViewModel dto = _mapper.Map<Post, PostItemViewModel>(post);
                posts.Add(dto);
            }
            return posts;
        }
        public async Task<int> GetLikesAsync(string id)
        {

            var likes = await _unitOfWork.PostsRepository.GetLikesByIdAsync(id);
            return likes;
        }
        public async Task<PostItemViewModel> LikePostAsync(string id)
        {
            var post = await _unitOfWork.PostsRepository.IncPostLikesAsync(id);
            PostItemViewModel dto = _mapper.Map<Post, PostItemViewModel>(post);

            return dto;
        }
        public async Task<PostItemViewModel> DislikePostAsync(string id)
        {
            var post = await _unitOfWork.PostsRepository.DicPostLikesAsync(id);
            PostItemViewModel dto = _mapper.Map<Post, PostItemViewModel>(post);

            return dto;
        }
    }
}
