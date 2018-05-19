using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InternetMagazine.Models
{
    public class OrderItemVIewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int RoomId { get; set; } 
        public RoomViewModel Room { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public int Count { get; set; }
    }
}