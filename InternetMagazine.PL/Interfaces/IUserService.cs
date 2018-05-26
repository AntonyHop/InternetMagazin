using InternetMagazine.PL.DTO;
using System.Collections.Generic;

namespace InternetMagazine.PL.Interfaces
{
    public interface IUserService
    {
        void RegistUser(UserDTO user);
        void RemoveUser(int? id);

        void ChangeStatus(int id,string role);
        bool LoginVerify(string username, string passwort);

        IEnumerable<UserDTO> GetUsers();
        UserDTO getUserByName(string name);
        UserDTO getUserById(int? id);
        void UpdateUser(UserDTO user);
        IEnumerable<OrderItemDTO> getOrdersByUserId(int? id, int? count);
    }
}
