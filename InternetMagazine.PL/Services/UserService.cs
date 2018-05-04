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

        }

        public void ChangeStatus(int id, string role)
        {
            User curr = Db.Users.Get(u => u.Id == id).LastOrDefault();
            if (curr == null)
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");

            curr.Role = role;

            Db.Users.Update(curr);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable <User> users = Db.Users.Get();

            if (users == null || users.Count() == 0)
                throw new UserNotFoundExaption("Пользователь не найден","UserService");

            return DtoToUser.Map< IEnumerable<User>, List<UserDTO>>(users);
        }

        public bool LoginVerify(string username, string passwort)
        {
            User curr = Db.Users.Get(u => u.NickName == username).LastOrDefault();

            if (curr == null)
                new UserNotFoundExaption("Пользователь не зарегестрирован", "UserService");

            passwort = Crypt.GetMd5Hash(passwort);
            if(passwort == curr.Password)
            {
                return true;
            }
            return false;
        }

        public void RegistUser(UserDTO user)
        {
            User curr = Db.Users.Get(u => u.NickName == user.NickName).LastOrDefault();
            if(curr != null)
                throw new UserNotFoundExaption("Пользователь c таким ником уже зарегестрирован", "UserService");

            curr = DtoToUser.Map<UserDTO, User>(user);

            Db.Users.Create(curr);
        }

        public void RemoveUser(UserDTO user)
        {
            User curr = Db.Users.Get(u => u.Id == user.Id).LastOrDefault();
            if (curr == null)
                throw new UserNotFoundExaption("Пользователь не найден", "UserService");

            Db.Users.Remove(curr);
        }
    }
}
