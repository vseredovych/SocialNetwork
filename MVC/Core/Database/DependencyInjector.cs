﻿//using Unity;
//using Unity.Resolution;

//namespace MVC.Database
//{
//    public static class DependencyInjector
//    {
//        private readonly static IUnityContainer _unityContainer = GetUnity();
//        private static IUnityContainer GetUnity()
//        {
//            var container = new UnityContainer();
//            container.RegisterDALTypes();

//            return container;
//        }

//        public static void RegisterDALTypes(this IUnityContainer container)
//        {
//            container
//                .RegisterType<IPostsRepository, PostsRepository>()
//                .RegisterType<IUsersRepository, UsersRepository>();
//        }

//        public static T Resolve<T>(params ParameterOverride[] overrides)
//        {
//            return _unityContainer.Resolve<T>(overrides);
//        }
//    }
//}
