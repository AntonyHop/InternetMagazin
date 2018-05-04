using System.ComponentModel.DataAnnotations;

namespace InternetMagazine.PL.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        
        public CategoryDTO Category { get; set; }
    }
}
