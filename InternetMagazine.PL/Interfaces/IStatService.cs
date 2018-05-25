using InternetMagazine.PL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.PL.Interfaces
{
    public interface IStatService
    {
        int CountProducts();
        int CountCategories();
        int CountOrders();
        int CountUsers();
        IEnumerable<ProductDTO> BestProducts(int limit);
    }
}
