using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.DAL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<OrderLine> Products { get; set; }

        public double Price { get; set; }
        public string Status { get; set; }
    }
}
