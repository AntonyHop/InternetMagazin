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
        IMapper DtoToUser;
        IMapper UserToDto;

        CryptLogic Crypt = new CryptLogic();

        public UserService(IUnitOfWork _db)
        {
            Db = _db;

            DtoToUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO,User>()).CreateMapper();
            UserToDto = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO > ()).CreateMapper();

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
                return DtoToUser.Map<IEnumerable<User>, List<UserDTO>>(users);
            }    
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
                return DtoToUser.Map<User, UserDTO>(curr);
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
                return DtoToUser.Map<User, UserDTO>(curr);
            }
        }

        public bool LoginVerify(string username, string passwort)
        {
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
            user.Password = Crypt.GetMd5Hash(user.Password);
            User curr = Db.Users.Get(u => u.NickName == user.NickName).LastOrDefault();
            if(curr != null)
            {
                throw new UserNotFoundExaption("Пользователь c таким ником уже зарегестрирован", "UserService");
            }
            else
            {
                curr = DtoToUser.Map<UserDTO, User>(user);
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
            curr = DtoToUser.Map<UserDTO, User>(user);
            Db.Users.Update(curr);
        }

        public void RemoveUser(int? id)
        {
            User curr = Db.Users.Get(u => u.Id == id).LastOrDefault();
            if (curr == null)
            {
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");
            }
            else
            {
                Db.Users.Remove(curr);
            }
        }
    }
}
