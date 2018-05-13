using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetMagazine.Models
{
    public class OrderItemVIewModel
    {
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }
    }
}