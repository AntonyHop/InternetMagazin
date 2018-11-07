
namespace InternetMagazine.PL.DTO
{
    public class UserDTO
    {
       
        public int Id { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string NickName { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string Mobule { get; set; }

        public string Role { get; set; }


        public override string ToString() {
            return this.FirstName + this.LastName;
        }
    }
}
