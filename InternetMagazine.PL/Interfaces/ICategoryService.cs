using System;
using InternetMagazine.PL.DTO;
using System.Collections.Generic;

namespace InternetMagazine.PL.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<EventDTO> LoadProductsCategory(int? catId);
        IEnumerable<EventDTO> Products();
        IEnumerable<EventDTO> Search(string q);
        IEnumerable<RoomDTO> Categories();
        EventDTO GetOneProduct(int? id);
        void EditCategory(int id, string name);
        void AddCategory(string name);
        void DeleteCategory(int id);
        void CreateOrder(OrderItemDTO or);
        IEnumerable<OrderItemDTO> GetOrders();
        void DellOrder(int? id);

        void AddProduct(EventDTO pr);
        void UpdateOneProduct(EventDTO p);
        void DeleteProduct(int? id);

        void Dispose();
    }
}
