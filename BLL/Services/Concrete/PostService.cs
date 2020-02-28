using AutoMapper;
using BLL.DTOs;
using DAL;
using DAL.Entities;
using System.Collections.Generic;

namespace BLL.Services
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

    }
}
