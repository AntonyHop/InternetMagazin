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
        IMapper categoryMap;
        IMapper productMap;

        public CategoryService(IUnitOfWork uow)
        {
            Db = uow;

            categoryMap = new MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();
            productMap = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>()).CreateMapper();
        }

        public IEnumerable<CategoryDTO> Categories()
        {
            
            return categoryMap.Map<IEnumerable<Category>, List<CategoryDTO>>(Db.Categories.Get());
        }

        public IEnumerable<ProductDTO> LoadProductsCategory(int? catId)
        {
            if (catId == null) 
                throw new ValidationException("Категории не существует", "CategoryService");

            IEnumerable<Product> products = Db.Products.GetWithInclude(p => p.CategoryId == catId, p => p.Category).OrderByDescending(p => p.Id);
        
            return productMap.Map<IEnumerable<Product>, List<ProductDTO>>(products); 
        }

        public IEnumerable<ProductDTO> Products()
        {
            IEnumerable<Product> products = Db.Products.GetWithInclude(p => p.Category).OrderByDescending(p => p.Id);
            return productMap.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        public ProductDTO GetOneProduct(int id)
        {
            Product geted = Db.Products.Get(p => p.Id == id).LastOrDefault();

            if (geted == null)
                throw new ValidationException("Товара не существует", "CategoryService");

            return productMap.Map<Product, ProductDTO>(geted);
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

        public void AddCategory(string name)
        {
            Db.Categories.Create(new Category() { Name = name });
        }

        public void EditCategory(int id, string name)
        {

            Category geted = Db.Categories.Get(c => c.Id == id).FirstOrDefault();
            if (geted == null)
            {
                throw new ValidationException("Категории нет", "CategoryService");
            }else if (geted.Name == name) {
                throw new ValidationException("Зачем чтото менять и так хорошо", "CategoryService");
            }
            else {
                geted.Name = name;
                Db.Categories.Update(geted);
            }   

        }

        public void DeleteCategory(int id)
        {

            Category geted = Db.Categories.Get(c => c.Id == id).FirstOrDefault();

            if (geted == null)
            {
                throw new ValidationException("Категории нет", "CategoryService");
            }
            else
            {
                IEnumerable<Product> prbycat = Db.Products.Get(P => P.CategoryId == id);

                if(prbycat.Count() > 0)
                {
                    foreach(Product p in prbycat)
                    {
                        p.CategoryId = 1;
                        Db.Products.Update(p);
                    }
                }

                Db.Categories.Remove(geted);
            }

        }
    }
}
