using InternetMagazine.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void AddOrder(List<OrderItemDTO> orders,UserDTO user)
        {
            foreach(OrderItemDTO or in orders)
            {
                Order o = new Order() { ProductId = or.Product.Id,Status="MakeOrder",Price = (double)(or.Count*or.Product.Price),Count = or.Count};

                o.UserId = user.Id;

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
