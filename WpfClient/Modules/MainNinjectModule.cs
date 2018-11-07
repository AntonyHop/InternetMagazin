using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Modules
{
    public class MainNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>().InTransientScope();
            Bind<IUserService>().To<UserService>().InTransientScope();
            Bind<IOrderService>().To<OrderService>().InTransientScope();
            Bind<IStatService>().To<StatServcie>().InTransientScope();
        }
    }
}
