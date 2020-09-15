using AutoMapper;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;
using Maximov.SunMessenger.Core.Users.Queries;
using Maxinov.SunMessenger.Services.DTO;
using Maxinov.SunMessenger.Services.UserService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maxinov.SunMessenger.Services.UserService
{
    public class UserService : Abstractions.IUserService
    {
        private readonly IMessengerUnit messengerUnit;
        private readonly IMapper mapper;

        public UserService(IMessengerUnit messengerUnit, IMapper mapper)
        {
            this.messengerUnit = messengerUnit;
            this.mapper = mapper;
        }

        #region Queries

        public UserDto FindById(Guid userId)
        {
            User user = messengerUnit.Users.FindById(userId);
            UserDto userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public UserDto FindByLogin(string login)
        {
            User user = messengerUnit.Users.GetAll().WithLogin(login);
            UserDto userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public UserDto FindByLoginAndPassword(string login, string password)
        {
            UserDto userDto = null;
            User user = messengerUnit.Users.GetAll().WithLogin(login);
            if (user!=null && user.Password == password)
                userDto = mapper.Map<UserDto>(user);

            return userDto;
        }

        #endregion

        #region Commands

        public UserDto Create(string login, string name, string password)
        {
            if (messengerUnit.Users.GetAll().WithLogin(login) != null)
                throw new ArgumentException("Такой логин уже занят");

            User user = new User(name, login, password);
            messengerUnit.Users.Create(user);
            messengerUnit.SaveChanges();

            UserDto userDto = mapper.Map<UserDto>(user);

            return userDto;
        }

        #endregion
    }
}
