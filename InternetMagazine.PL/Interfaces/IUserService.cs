using InternetMagazine.PL.DTO;
using System.Collections.Generic;

namespace InternetMagazine.PL.Interfaces
{
    public interface IUserService
    {
        void RegistUser(UserDTO user);
        void RemoveUser(UserDTO user);

        void ChangeStatus(int id,string role);
        bool LoginVerify(string username, string passwort);

        IEnumerable<UserDTO> GetUsers();
        


    }
}
