using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetMagazine.PL.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int RoomId { get; set; }
        public RoomDTO Room { get; set; }
        
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}
