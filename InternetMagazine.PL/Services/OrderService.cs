using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.PL.Services
{
    public class OrderService : IOrderService
    {
        public UserDTO User { get; set; }
        public IEnumerable<OrderItemDTO> Lines
        {
            get { return chart; }
        }

        private List<OrderItemDTO> chart = new List<OrderItemDTO>();

        public void AddItem(ProductDTO product, int count)
        {
            OrderItemDTO line = chart.Where(g => g.Product.Id == product.Id).FirstOrDefault();

            if (line == null)
            {
                chart.Add(new OrderItemDTO
                {
                    Product = product,
                    Count = count
                });
            }
            else
            {
                line.Count += count;
            }
        }

       public void PlusItem(int id)
        {
            OrderItemDTO line = chart.Where(g => g.Product.Id == id).FirstOrDefault();
            if(line != null)
            {
                line.Count++;
            }
        }

        public void MinusItem(int id)
        {
            OrderItemDTO line = chart.Where(g => g.Product.Id == id).FirstOrDefault();
            if (line != null)
            {
                if (line.Count > 0)
                {
                    line.Count--;
                }
                else
                {
                    RemoveLine(id);
                }
            }
        }


        public void RemoveLine(ProductDTO game)
        {
            chart.RemoveAll(l => l.Product.Id == game.Id);
        }

        public void RemoveLine(int idGame)
        {
            chart.RemoveAll(l => l.Product.Id == idGame);
        }

        public decimal ComputeTotalValue()
        {
            return chart.Sum(e => e.Product.Price * e.Count);

        }

        public void Clear()
        {
            chart.Clear();
        }

    }
}
