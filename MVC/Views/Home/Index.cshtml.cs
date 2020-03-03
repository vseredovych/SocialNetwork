using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MVC.Models.BLL.DTOs;
using MVC.Models.DAL;
using MVC.Models.DAL.Entities;
using System;
using System.Collections.Generic;

namespace MVC.Views.Pages
{
    public class PostService : PageModel
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


