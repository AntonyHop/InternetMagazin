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

        public void CreateOrder(OrderItemDTO or)
        {
            Order curr = Db.Orders.Get(o => (o.Date == or.Date) && (o.RoomId == or.RoomId)).FirstOrDefault();
            if(curr == null)
            {
                Db.Orders.Create(map.Map<OrderItemDTO, Order>(or));
            }
            else
            {
                throw new ValidationException("Комната на указаное время занята", "categoryservice");
            }
          
        }

        public void DellOrder(int? id)
        {
            Order geted = Db.Orders.Get(c => c.Id == id).FirstOrDefault();

            if (geted == null)
            {
                throw new ValidationException("Заказа нет", "CategoryService");
            }
            Db.Orders.Remove(geted);
        }

        public IEnumerable<OrderItemDTO> GetOrders()
        {
            return map.Map<IEnumerable<Order>, List<OrderItemDTO>>(Db.Orders.GetWithInclude(c=>c.Room));
        }

        public IEnumerable<RoomDTO> Categories()
        {
            
            return map.Map<IEnumerable<Room>, List<RoomDTO>>(Db.Categories.Get());
        }

        public IEnumerable<EventDTO> LoadProductsCategory(int? catId)
        {
            if (catId == null) 
                throw new ValidationException("Категории не существует", "CategoryService");

            IEnumerable<Event> products = Db.Products.GetWithInclude(p => p.CategoryId == catId, p => p.Category).OrderByDescending(p => p.Id);
        
            return map.Map<IEnumerable<Event>, List<EventDTO>>(products); 
        }

        public IEnumerable<EventDTO> Products()
        {
            IEnumerable<Event> products = Db.Products.GetWithInclude(p => p.Category).OrderByDescending(p => p.Id);
            return map.Map<IEnumerable<Event>, List<EventDTO>>(products);
        }

        public EventDTO GetOneProduct(int? id)
        {
            if(id != null)
            {
                Event geted = Db.Products.Get(p => p.Id == id).LastOrDefault();

                if (geted == null)
                    throw new ValidationException("Товара не существует", "CategoryService");

                return map.Map<Event, EventDTO>(geted);
            }
            else
            {
                throw new ValidationException("Товара не существует", "CategoryService");
            }
           
        }

        public void UpdateOneProduct(EventDTO p)
        {

            if (p.Name.Length > 20)
                throw new ValidationException("Слижком большое название товара", "Category service");
            if (p.Desc.Length > 255)
                throw new ValidationException("Слижком большое описание товара", "Category service");

            Event geted = Db.Products.Get(c => c.Id == p.Id).FirstOrDefault();
          
            if (geted != null)
            {
                Db.Products.Update(map.Map<EventDTO, Event>(p));
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
                Db.Categories.Create(new Room() { Name = name });
            }
            else
                throw new ValidationException("Слижком большое название категории","Category service");
           
        }

        public void AddProduct(EventDTO pr)
        {
            if(pr.Name.Length > 20)
                throw new ValidationException("Слижком большое название товара", "Category service");
            if (pr.Desc.Length > 255)
                throw new ValidationException("Слижком большое описание товара", "Category service");

            var Product = map.Map<EventDTO,Event>(pr);
            Db.Products.Create(Product);
        }
      
        

        public void EditCategory(int id, string name)
        {

            Room geted = Db.Categories.Get(c => c.Id == id).FirstOrDefault();
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
            Event geted = Db.Products.Get(c => c.Id == id).FirstOrDefault();

            if (geted == null)
            {
                throw new ValidationException("Товара нет", "CategoryService");
            }
            Db.Products.Remove(geted);
        }

            public void DeleteCategory(int id)
        {

            Room geted = Db.Categories.Get(c => c.Id == id).FirstOrDefault();

            if (geted == null)
            {
                throw new ValidationException("Категории нет", "CategoryService");
            }
            else
            {
                IEnumerable<Event> prbycat = Db.Products.Get(P => P.CategoryId == id);

                if(prbycat.Count() > 0)
                {
                    foreach(Event p in prbycat)
                    {
                        p.CategoryId = 1;
                        Db.Products.Update(p);
                    }
                }

                Db.Categories.Remove(geted);
            }

        }

        public IEnumerable<EventDTO> Search(string q)
        {
            IEnumerable<Event> events = Db.Products.GetWithInclude(n => n.Category).Where(n=> n.Name.Contains(q));

            if(events.Count() == 0)
            {
                throw new ValidationException("Not Found", "category service");
            }
            else
            {
               return  map.Map<IEnumerable<Event>, List<EventDTO>>(events);
            }
        }
    }
}
