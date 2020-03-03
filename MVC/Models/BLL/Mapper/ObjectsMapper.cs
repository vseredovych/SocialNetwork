using AutoMapper;
using MVC.Models.BLL.DTOs;
using MVC.Models.DAL.Entities;

namespace MVC.Models.BLL.Mapper
{
    public static class ObjectsMapper
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostDTO, Post>();
                cfg.CreateMap<Post, PostDTO>();
                //cfg.CreateMap<Order, OrderDTO>();
                //cfg.CreateMap<OrderDTO, OrderViewModel>()
                //   .ForMember(x => x.User,
                //               m => m.MapFrom(y => y.User.Email));
                //cfg.CreateMap<OrderViewModel, Order>();
            }).CreateMapper();
        }
    }
}
