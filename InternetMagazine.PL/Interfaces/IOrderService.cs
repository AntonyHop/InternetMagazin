using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.PL.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(IEnumerable<OrderItemDTO> orders, double total_price);
        void Delete(int id);
        void SetStatus(int id, string status);
    }
}
