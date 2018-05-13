using Ninject.Modules;
using System;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Services;

namespace InternetMagazine.Util
{
    public class CategoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IUserService>().To<UserService>();
           
        }
    }
} 