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
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
