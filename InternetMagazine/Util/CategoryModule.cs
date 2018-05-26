using Ninject.Modules;
using System;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Services;
using System.Web.Http.Dependencies;
using Ninject;

namespace InternetMagazine.Util
{
    public class CategoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IUserService>().To<UserService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IStatService>().To<StatServcie>();
        }
    }

    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(this.kernel.BeginBlock());
        }
    }
} 
