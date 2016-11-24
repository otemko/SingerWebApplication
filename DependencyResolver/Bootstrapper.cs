using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using ORMSinger;
using System.Data.Entity;
using DALSinger;
using BLLSinger.Interfaces;
using BLLSinger.Services;

namespace DependencyResolver
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            System.Web.Mvc.DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            
            container.RegisterType<DbContext, SingerDbContext>();
            container.RegisterType<IRepository, Repository>();
            container.RegisterType<ISingerService, SingerService>();
            container.RegisterType<ISongService, SongService>();
            container.RegisterType<IAccordService, AccordService>();

            RegisterTypes(container);
            return container;
        }
        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
