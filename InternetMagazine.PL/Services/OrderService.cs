using InternetMagazine.PL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using InternetMagazine.PL.DTO;
using InternetMagazine.DAL.Interfaces;
using AutoMapper;
using InternetMagazine.PL.Infrastructure;
using InternetMagazine.DAL.Entities;

namespace InternetMagazine.PL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork uofw;
        MapperConfiguration config = new AutoMapperConfiguration().Configure();
        IMapper map;

        public OrderService (IUnitOfWork _uofw)
        {
            uofw = _uofw;
            map = config.CreateMapper();
        }

        public IEnumerable<OrderItemDTO> Orders()
        {
            IEnumerable<Order> or = uofw.Orders.GetWithInclude(o => o.Product, o => o.User);
            if (or == null)
                throw new ValidationException("not found orders", "OrderService");

            return map.Map<IEnumerable<Order>, List<OrderItemDTO>>(or);
        }

        public void AddOrder(List<OrderItemDTO> orders,UserDTO user)
        {
            foreach(OrderItemDTO or in orders)
            {
                Order o = new Order() { ProductId = or.Product.Id,Status="MakeOrder",Price = (double)(or.Count*or.Product.Price),Count = or.Count};

                o.UserId = user.Id;
                //Для статистики считаем количество покупок товара.
                Product p = uofw.Products.Get(i => i.Id == or.Product.Id).FirstOrDefault();
                p.CountOfpay++;
                uofw.Products.Update(p);

                uofw.Orders.Create(o);
            }
            
        }

       

        public void Delete(int id)
        {
            Order o = uofw.Orders.Get(i => i.Id == id).FirstOrDefault();
            if(o != null)
            {
                uofw.Orders.Remove(o);
            }
            else
            {
                throw new ValidationException("Заказа не существует", "OrderService");
            }
        }

        public IEnumerable<OrderItemDTO> getOrdersByUserId(int? id, int? count)
        {
            if (id == null)
                throw new ValidationException("Bad Params", "OrderService");
            IEnumerable<Order> orders = null;
            if (count == null)
            {
                orders = uofw.Orders.Get(o => o.UserId == id);
            }
            else
            {
                orders = uofw.Orders.Get(o => o.UserId == id).Take(count.Value);
            }
            

            if (orders.Count() == 0)
                throw new ValidationException("Orders not found", "OrderService");

            return map.Map<IEnumerable<Order>, List<OrderItemDTO>>(orders);
        }

        public void SetStatus(int id, string status)
        {
            Order o = uofw.Orders.Get(i => i.Id == id).FirstOrDefault();
            if (o != null)
            {
                o.Status = status;
                uofw.Orders.Update(o);
            }
            else
            {
                throw new ValidationException("Заказа не существует", "OrderService");
            }
        }
    }
}