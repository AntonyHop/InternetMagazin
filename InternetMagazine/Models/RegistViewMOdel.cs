using System.ComponentModel.DataAnnotations;


namespace InternetMagazine.Models
{
    public class RegistViewModel
    {
        
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        public string NickName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Text)]
        public int Age { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Mobule { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [DataType(DataType.Text)]
        public string Role { get; set; }

    }
}