using System.ComponentModel.DataAnnotations;

namespace InternetMagazine.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        
        public CategoryViewModel Category { get; set; }
    }
}
