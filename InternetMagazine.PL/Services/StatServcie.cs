using InternetMagazine.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetMagazine.PL.DTO;
using InternetMagazine.DAL.Interfaces;
using InternetMagazine.DAL.Entities;
using InternetMagazine.PL.Infrastructure;
using AutoMapper;

namespace InternetMagazine.PL.Services
{
    public class StatServcie : IStatService
    {
        IUnitOfWork uofw;
        MapperConfiguration config = new AutoMapperConfiguration().Configure();
        IMapper map;

        public StatServcie(IUnitOfWork _uofw)
        {
            uofw = _uofw;
            map = config.CreateMapper();
        }
        public IEnumerable<ProductDTO> BestProducts(int limit)
        {
            IEnumerable <Product> best_products = uofw.Products.Get().OrderByDescending(i => i.CountOfpay).Take(limit);

            if(best_products == null)
                throw new ValidationException("Продуктов не найдено", "StatServcie");

            return map.Map<IEnumerable<Product>, List<ProductDTO>>(best_products);
        }

        public int CountCategories()
        {
            return uofw.Categories.Get().Count();
        }

        public int CountOrders()
        {
            return uofw.Orders.Get().Count();
        }

        public int CountProducts()
        {
            return uofw.Products.Get().Count();
        }

        public int CountUsers()
        {
            return uofw.Users.Get().Count();
        }
    }
}
