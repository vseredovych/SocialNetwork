using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BLL.Services
{
    public class PostService : PageModel, IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<PostDTO> GetAll()
        {
            List<PostDTO> posts = new List<PostDTO>();

            foreach (var post in _unitOfWork.PostsRepository.GetAll())
            {
                PostDTO dto = _mapper.Map<Post, PostDTO>(post);
                posts.Add(dto);
            }
            return posts;
        }
        public int GetLikes(string id)
        {
            return _unitOfWork.PostsRepository.DicPostLikes(id).Likes;
        }
        public int LikePost(string id)
        {
            return _unitOfWork.PostsRepository.IncPostLikes(id).Likes;
        }
        public int DislikePost(string id)
        {
            return _unitOfWork.PostsRepository.DicPostLikes(id).Likes;
        }
    }
}
