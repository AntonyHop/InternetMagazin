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
    class OrderService : IOrderService
    {
        IUnitOfWork uofw;
        MapperConfiguration config = new AutoMapperConfiguration().Configure();
        IMapper map;

        public OrderService (IUnitOfWork _uofw)
        {
            uofw = _uofw;
            map = config.CreateMapper();
        }
        public void AddOrder(IEnumerable<OrderItemDTO> orders, double total_price)
        {
            Order order = new Order();
            order.Price = total_price;
            order.Products = map.Map<IEnumerable<OrderItemDTO>, List<OrderLine>>(orders);
            order.Status = "Ordered";

            uofw.Orders.Create(order);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(int id, string status)
        {
            throw new NotImplementedException();
        }
    }
}
