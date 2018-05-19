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
        MapperConfiguration config = new AutoMapperConfiguration().Configure();
        IMapper map;

        public CategoryService(IUnitOfWork uow)
        {
            Db = uow;
            map = config.CreateMapper();

        }

        public IEnumerable<CategoryDTO> Categories()
        {
            
            return map.Map<IEnumerable<Category>, List<CategoryDTO>>(Db.Categories.Get());
        }

        public IEnumerable<ProductDTO> LoadProductsCategory(int? catId)
        {
            if (catId == null) 
                throw new ValidationException("Категории не существует", "CategoryService");

            IEnumerable<Product> products = Db.Products.GetWithInclude(p => p.CategoryId == catId, p => p.Category).OrderByDescending(p => p.Id);
        
            return map.Map<IEnumerable<Product>, List<ProductDTO>>(products); 
        }

        public IEnumerable<ProductDTO> Products()
        {
            IEnumerable<Product> products = Db.Products.GetWithInclude(p => p.Category).OrderByDescending(p => p.Id);
            return map.Map<IEnumerable<Product>, List<ProductDTO>>(products);
        }

        public ProductDTO GetOneProduct(int? id)
        {
            if(id != null)
            {
                Product geted = Db.Products.Get(p => p.Id == id).LastOrDefault();

                if (geted == null)
                    throw new ValidationException("Товара не существует", "CategoryService");

                return map.Map<Product, ProductDTO>(geted);
            }
            else
            {
                throw new ValidationException("Товара не существует", "CategoryService");
            }
           
        }

        public void UpdateOneProduct(ProductDTO p)
        {

            if (p.Name.Length > 20)
                throw new ValidationException("Слижком большое название товара", "Category service");
            if (p.Desc.Length > 255)
                throw new ValidationException("Слижком большое описание товара", "Category service");

            Product geted = Db.Products.Get(c => c.Id == p.Id).FirstOrDefault();
          
            if (geted != null)
            {
                Db.Products.Update(map.Map<ProductDTO, Product>(p));
            }
            else
            {
                throw new ValidationException("Товара не существует", "CategoryService");
            }

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
            if (name.Length <= 20)
            {
                Db.Categories.Create(new Category() { Name = name });
            }
            else
                throw new ValidationException("Слижком большое название категории","Category service");
           
        }

        public void AddProduct(ProductDTO pr)
        {
            if(pr.Name.Length > 20)
                throw new ValidationException("Слижком большое название товара", "Category service");
            if (pr.Desc.Length > 255)
                throw new ValidationException("Слижком большое описание товара", "Category service");

            var Product = map.Map<ProductDTO,Product>(pr);
            Db.Products.Create(Product);
        }


        public IEnumerable<ProductDTO> Search(string s)
        {
            IEnumerable<Product> events = Db.Products.GetWithInclude(n => n.Category).Where(n => n.Name.Contains(s));

            if (events.Count() == 0)
            {
                throw new ValidationException("Not Found", "category service");
            }
            else
            {
                return map.Map<IEnumerable<Product>, List<ProductDTO>>(events);
            }

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

        public void DeleteProduct(int? id)
        {
            Product geted = Db.Products.Get(c => c.Id == id).FirstOrDefault();

            if (geted == null)
            {
                throw new ValidationException("Товара нет", "CategoryService");
            }
            Db.Products.Remove(geted);
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
