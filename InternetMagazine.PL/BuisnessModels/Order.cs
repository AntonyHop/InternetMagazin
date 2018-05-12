using InternetMagazine.PL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.PL.BuisnessModels
{
    public class Order
    {
        private List<OrderItem> chart = new List<OrderItem>();

        public void AddItem(ProductDTO product, int count)
        {
            OrderItem line = chart
                .Where(g => g.Procuct.Id == product.Id)
                .FirstOrDefault();

            if (line == null)
            {
                chart.Add(new OrderItem
                {
                    Procuct = product,
                    Count = count
                });
            }
            else
            {
                line.Count += count;
            }
        }

        public IEnumerable<OrderItem> Lines
        {
            get { return chart; }
        }
    }

    public class OrderItem {
        public ProductDTO Procuct { get; set; }
        public int Count { get; set; }
    }
}
