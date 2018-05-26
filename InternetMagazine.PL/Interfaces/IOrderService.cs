using InternetMagazine.PL.DTO;
using System.Collections.Generic;


namespace InternetMagazine.PL.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(List<OrderItemDTO> orders, UserDTO user);
        IEnumerable<OrderItemDTO> Orders();
        void Delete(int id);
        void SetStatus(int id, string status);
        IEnumerable<OrderItemDTO> getOrdersByUserId(int? id, int? count);
    }
}
