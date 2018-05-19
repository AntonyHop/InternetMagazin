using System.ComponentModel.DataAnnotations;
using System.Web;

namespace InternetMagazine.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Author { get; set; }

        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }

        [DataType(DataType.PhoneNumber)]
        public decimal Price { get; set; }

        public int? CategoryId { get; set; }
        
        public RoomViewModel Category { get; set; }

        public string ImgUrl { get; set; }

        public HttpPostedFileBase File { get; set; }

    }
}
