using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetMagazine.Models
{
    public class OrderItemVIewModel
    {
        public int Id { get; set; }
        public ProductViewModel Product { get; set; }
        public RegistViewModel User { get; set; }
        public int Count { get; set; }
    }
}