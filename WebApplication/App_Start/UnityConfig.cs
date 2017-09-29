using System;
using System.Web.Http;
using System.Web.Mvc;
using Contracts.Repositories;
using Contracts.Services;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Repositories;
using Repositories.Repositories;
using SecurityLibrary;

namespace WebApplication
{
    public class UnityConfig
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();

            RegisterTypes(container);
            
            //since we using both web api and mvc
            
            //using Microsoft.Practices.Unity.Mvc.UnityDependencyResolver
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<AppDbContext>(new PerResolveLifetimeManager(), new InjectionConstructor());
            
            RegisterRepositories(container);
            RegisterServices(container);
        }

        private static void RegisterRepositories(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IUserRepository, UserRepository>();
        }

        private static void RegisterServices(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IAuthenticationServices, AuthenticationServices>();
        }
    }
}