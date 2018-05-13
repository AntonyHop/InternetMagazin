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
        UserDTO User { get; set; }
        void AddItem(ProductDTO product, int count);
        IEnumerable<OrderItemDTO> Lines { get; }
        void RemoveLine(ProductDTO game);
        void RemoveLine(int gameId);
        decimal ComputeTotalValue();
        void PlusItem(int id);
        void MinusItem(int id);

        void MakeOrder();

        void Clear();
    }
}
