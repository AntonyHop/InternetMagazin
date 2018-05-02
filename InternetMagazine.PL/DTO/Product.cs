using System.ComponentModel.DataAnnotations;

namespace InternetMagazine.DAL.Entities
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public double Price { get; set; }

        public int? CategoryId { get; set; }
        
        public CategoryDTO Category { get; set; }
    }
}
