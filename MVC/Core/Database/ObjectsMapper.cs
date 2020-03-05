using AutoMapper;
using MVC.Core.Entities;
using MVC.ViewModels;
using MVC.Web.ViewModels;

namespace MVC.Database
{
    public static class ObjectsMapper
    {
        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PostViewModel, Post>();
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<UserViewModel, User>();

                //cfg.CreateMap<User, UserViewModel>()
                //.ForMember(x => x.Id,
                //           m => m.MapFrom(y => y._id));
                //cfg.CreateMap<Order, OrderDTO>();
                //cfg.CreateMap<OrderDTO, OrderViewModel>()
                //   .ForMember(x => x.User,
                //               m => m.MapFrom(y => y.User.Email));
                //cfg.CreateMap<OrderViewModel, Order>();
            }).CreateMapper();
        }
    }
}
