using System;
using System.Collections.Generic;
using InternetMagazine.DAL.Entities;
using InternetMagazine.DAL.Interfaces;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Infrastructure;
using AutoMapper;
using System.Linq;

namespace InternetMagazine.PL.Services
{
    public class CategoryService : ICategoryService
    {
        IUnitOfWork Db { get; set; }
        public CategoryService(IUnitOfWork uow)
        {
            Db = uow;
        }

        public IEnumerable<CategoryDTO> Categories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Db.Categories.Get());
            
        }

        public IEnumerable<ProductDTO> LoadProductsCategory(int? catId)
        {
            if (catId == null) 
                throw new ValidationException("Категории не существует","");

            IEnumerable<Product> products = Db.Products.GetWithInclude(p => p.CategoryId == catId, p => p.Category).OrderByDescending(p => p.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        public IEnumerable<ProductDTO> Products()
        {
            IEnumerable<Product> products = Db.Products.GetWithInclude(p => p.Category).OrderByDescending(p => p.Id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        public ProductDTO GetOneProduct(int id)
        {
            Product geted = Db.Products.Get(p => p.Id == id).LastOrDefault();

            if (geted == null)
                throw new ValidationException("Товара не существует", "");

            return Mapper.Map<Product, ProductDTO>(geted);
        }

        public static string CutText(string full_text)
        {
            List<char> list = full_text.ToList();
            list = list.Take(50).ToList();
            string res = "";
            foreach (char c in list)
            {
                res += c;
            }
            res += "...";

            return res;
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
