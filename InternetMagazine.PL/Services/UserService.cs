using System;
using System.Collections.Generic;
using System.Linq;
using InternetMagazine.PL.DTO;
using InternetMagazine.PL.Interfaces;
using InternetMagazine.PL.Infrastructure;
using InternetMagazine.DAL.Interfaces;
using InternetMagazine.DAL.Entities;
using AutoMapper;

namespace InternetMagazine.PL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Db { get; set; }
        IMapper DtoToUser;
        IMapper UserToDto;

        public UserService(IUnitOfWork _db)
        {
            Db = _db;

            DtoToUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO,User>()).CreateMapper();
            UserToDto = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();

        }

        public void ChangeStatus(int id, string role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            IEnumerable <User> users = Db.Users.Get();

            if (users == null || users.Count() == 0)
                throw new UserNotFoundExaption("Пользователь не найден","UserService");

            return DtoToUser.Map< IEnumerable<User>, List<UserDTO>>(users);
        }

        public void Login(string username, string passwort)
        {
            throw new NotImplementedException();
        }

        public void RegistUser(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
