#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.DependencyResolution
// Author	: Rod Johnson
// Created	: 03-19-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-21-2013
// ***********************************************************************
#endregion
#region

using System;
using System.Web;
using System.Web.Mvc;
using CoolApp.DependencyResolution;
using CoolApp.Infraestructure.Data;
using CoolApp.Infraestructure.Security;
using CoolApp.Core.Interfaces.Data;
using CoolApp.Core.Interfaces.Service;
using CoolApp.Core.Services;
using CoolApp.DependencyResolution;
using CoolApp.Infraestructure.Data;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

#endregion

[assembly: WebActivator.PreApplicationStartMethod(typeof(AppStartup), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(AppStartup), "Stop")]

namespace CoolApp.DependencyResolution
{
    #region

    

    #endregion

    public class AppStartup
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            // This is needed due to changed dependency resolution for WebAPI
            DependencyResolver.SetResolver(new NinjectResolver(kernel));

            // We don't need this, plus it messes up when controller is not found
            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));

            RegisterServices(kernel);
            return kernel;
        }

       

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // infrastructure
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();

            kernel.Bind<IAuthenticationService>().To<AuthenticationService>().InRequestScope();

            kernel.Bind<IChatRepository>().To<ChatRepository>().InRequestScope();
            kernel.Bind<IChatService>().To<ChatService>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IFriendshipRepository>().To<FriendshipRepository>().InRequestScope();
            kernel.Bind<IFriendshipService>().To<FriendshipService>().InRequestScope();
        }

        public class NinjectControllerFactory : DefaultControllerFactory
        {
            private readonly IKernel _ninjectKernel;

            public NinjectControllerFactory(IKernel kernel)
            {
                _ninjectKernel = kernel;
            }

            protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
            {
                return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);
            }
        }
    }
}