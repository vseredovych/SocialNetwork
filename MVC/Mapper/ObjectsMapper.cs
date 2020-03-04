using AutoMapper;
using MVC.DAL.Entities;
using MVC.ViewModels;

namespace MVC.Models.Mapper
{
    public static class ObjectsMapper
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostViewModel, Post>();
                cfg.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.Id,
                           m => m.MapFrom(y => y._id));
                //cfg.CreateMap<Order, OrderDTO>();
                //cfg.CreateMap<OrderDTO, OrderViewModel>()
                //   .ForMember(x => x.User,
                //               m => m.MapFrom(y => y.User.Email));
                //cfg.CreateMap<OrderViewModel, Order>();
            }).CreateMapper();
        }
    }
}
