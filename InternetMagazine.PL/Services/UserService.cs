using System;
using System.Collections.Generic;
using System.Linq;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Infrastructure;
using InternetMagazine.DAL.Interfaces;
using InternetMagazine.DAL.Entities;
using InternetMagazine.PL.BuisnessModels;
using AutoMapper;

namespace InternetMagazine.PL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Db { get; set; }
        MapperConfiguration config = new AutoMapperConfiguration().Configure();
        IMapper map;

        CryptLogic Crypt = new CryptLogic();

        public UserService(IUnitOfWork _db)
        {
            Db = _db;

            map = config.CreateMapper();

        }

        public void ChangeStatus(int id, string role)
        {
            User curr = Db.Users.Get(u => u.Id == id).LastOrDefault();
            if (curr == null)
            {
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");
            }else{
                curr.Role = role;
                Db.Users.Update(curr);
            }
            
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable <User> users = Db.Users.Get();

            if (users == null || users.Count() == 0)
            {
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");
            }
            else
            {
                return map.Map<IEnumerable<User>, List<UserDTO>>(users);
            }    
        }

        public IEnumerable<OrderItemDTO> getOrdersByUserId(int? id, int? count)
        {
            if (id == null)
                throw new ValidationException("Bad Params", "OrderService");
            IEnumerable<Order> orders = null;
            if (count == null)
            {
                orders = Db.Orders.GetWithInclude(u => u.UserId == id, c => c.Product).OrderByDescending(i => i.Id);
            }
            else
            {
                orders = Db.Orders.GetWithInclude(u => u.UserId == id, c => c.Product).Take(count.Value).OrderByDescending(i=>i.Id);
            }


            if (orders.Count() == 0)
                throw new ValidationException("Orders not found", "OrderService");

            return map.Map<IEnumerable<Order>, List<OrderItemDTO>>(orders);
        }

        public UserDTO getUserByName(string name)
        {
            User curr = Db.Users.Get(u => u.NickName == name).LastOrDefault();
            if(curr == null)
            {
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");
            }
            else
            {
                return map.Map<User, UserDTO>(curr);
            }
        }

        public UserDTO getUserById(int? id)
        {
            if(id == null)
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");

            User curr = Db.Users.Get(u => u.Id == id).LastOrDefault();
            if (curr == null)
            {
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");
            }
            else
            {
                return map.Map<User, UserDTO>(curr);
            }
        }

        public bool LoginVerify(string username, string passwort)
        {
            Console.WriteLine(Crypt.GetMd5Hash(passwort));
            User curr = Db.Users.Get(u => u.NickName == username).FirstOrDefault();
            if (curr == null)
            {
                throw new UserNotFoundExaption("Пользователь не зарегестрирован", "UserService");

            }else{
                passwort = Crypt.GetMd5Hash(passwort);
                if (passwort == curr.Password)
                {
                    return true;
                }
            }
            return false;
        }

        public void RegistUser(UserDTO user)
        {
            if(user.Password.Length < 4)
                throw new UserNotFoundExaption("Пароль слижком мал", "UserService");
            user.Password = Crypt.GetMd5Hash(user.Password);
            User curr = Db.Users.Get(u => u.NickName == user.NickName).LastOrDefault();
            if(curr != null)
            {
                throw new UserNotFoundExaption("Пользователь c таким ником уже зарегестрирован", "UserService");
            }
            else
            {
                curr = map.Map<UserDTO, User>(user);
                Db.Users.Create(curr);
            }

        }

        public void UpdateUser(UserDTO user)
        {

            User curr = Db.Users.Get(u => u.NickName == user.NickName).LastOrDefault();
         
            if(user.Role == null || user.Role == "")
            {
                user.Role = curr.Role;
            }
          
            if (user.Password == null || user.Password == "")
            {
                user.Password = curr.Password;
            }
            else
            {
                user.Password = Crypt.GetMd5Hash(user.Password);
            }
            curr = map.Map<UserDTO, User>(user);
            Db.Users.Update(curr);
        }

        public void RemoveUser(int? id)
        {
            User curr = Db.Users.Get(u => u.Id == id).LastOrDefault();
            if (curr == null)           
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");

            IEnumerable<Order> or = Db.Orders.Get(o => o.UserId == curr.Id);
            if(or.Count() != 0)
            {
                foreach(Order o in or)
                {
                    Db.Orders.Remove(o);
                }
            }

            Db.Users.Remove(curr);
            
        }
    }
}
