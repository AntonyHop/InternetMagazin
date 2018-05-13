using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.PL.DTO
{
    public class OrderItemDTO
    {
        public ProductDTO Product { get; set; }
        public int Count { get; set; }
    }
}
