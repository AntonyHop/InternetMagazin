using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InternetMagazine.DAL.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

    }
}
