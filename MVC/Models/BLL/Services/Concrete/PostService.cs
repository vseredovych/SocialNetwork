
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Models.DAL;
using MVC.Models.DAL.Entities;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Models.BLL.Services
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
        public async Task<IEnumerable<PostItemViewModel>> GetAll()
        {
            List<PostItemViewModel> posts = new List<PostItemViewModel>();

            foreach (var post in await _unitOfWork.PostsRepository.GetAll())
            {
                PostItemViewModel dto = _mapper.Map<Post, PostItemViewModel>(post);
                posts.Add(dto);
            }
            return posts;
        }
        public async Task<int> GetLikes(string id)
        {

            var likes = await _unitOfWork.PostsRepository.GetLikesById(id);
            return likes;
        }
        public async Task<PostItemViewModel> LikePost(string id)
        {
            var post = await _unitOfWork.PostsRepository.IncPostLikes(id);
            PostItemViewModel dto = _mapper.Map<Post, PostItemViewModel>(post);

            return dto;
        }
        public async Task<PostItemViewModel> DislikePost(string id)
        {
            var post = await _unitOfWork.PostsRepository.DicPostLikes(id);
            PostItemViewModel dto = _mapper.Map<Post, PostItemViewModel>(post);

            return dto;
        }
    }
}
