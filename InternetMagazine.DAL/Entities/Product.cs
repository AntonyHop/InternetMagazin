using System.ComponentModel.DataAnnotations;

namespace InternetMagazine.DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(1055)]
        public string Desc { get; set; }

        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        
        public Category Category { get; set; }

        [MaxLength(50)]
        public string ImgUrl { get; set; }

        public int CountOfpay { get; set; }
    }
}
