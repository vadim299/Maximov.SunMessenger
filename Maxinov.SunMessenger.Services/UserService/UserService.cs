using AutoMapper;
using Maximov.SunMessenger.Core.Interfaces;
using Maximov.SunMessenger.Core.Users.Objects;
using Maximov.SunMessenger.Data.MessengerUnitOfWork.Users.Specifications;
using Maximov.SunMessenger.Services.DTO;
using Maximov.SunMessenger.Services.UserService.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maximov.SunMessenger.Services.UserService
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

        public IEnumerable<UserDto> GetUsers(params Guid[] userIds)
        {
            var specification = new GetUsersByIdSpecification(userIds);
            var users = messengerUnit.Users.GetBySpecification(specification);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public UserDto FindByLogin(string login)
        {
            var specification = new GetUserByLoginSpecification(login);
            User user = messengerUnit.Users.GetBySpecification(specification).FirstOrDefault();
            UserDto userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public UserDto FindByLoginAndPassword(string login, string password)
        {
            UserDto userDto = null;
            var specification = new GetUserByLoginSpecification(login);
            User user = messengerUnit.Users.GetBySpecification(specification).FirstOrDefault();
            if (user!=null && user.Password == password)
                userDto = mapper.Map<UserDto>(user);

            return userDto;
        }

        #endregion

        #region Commands

        public UserDto Create(string login, string name, string password)
        {
            var specification = new GetUserByLoginSpecification(login);
            User sameUser = messengerUnit.Users.GetBySpecification(specification).FirstOrDefault();

            if (sameUser != null)
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
