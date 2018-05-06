using System.ComponentModel.DataAnnotations;
using System.Web;

namespace InternetMagazine.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }

        [DataType(DataType.PhoneNumber)]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        
        public CategoryViewModel Category { get; set; }

        public string ImgUrl { get; set; }

        public HttpPostedFileBase File { get; set; }

    }
}
