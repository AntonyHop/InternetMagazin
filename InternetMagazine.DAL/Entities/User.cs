using System.ComponentModel.DataAnnotations;

namespace InternetMagazine.DAL.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string Email { get; set; }

        [MaxLength(20)]
        [Required]
        public string NickName { get; set; }

        [MaxLength(20)]
        [Required]
        public string Password { get; set; }

        [MaxLength(3)]
        public int Age { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(15)]
        public string Mobule { get; set; }

        [MaxLength(20)]
        public string Role { get; set; }
    }
}
