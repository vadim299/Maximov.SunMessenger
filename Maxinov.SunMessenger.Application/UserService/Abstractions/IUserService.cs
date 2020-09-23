using Maxinov.SunMessenger.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maxinov.SunMessenger.Services.UserService.Abstractions
{
    public interface IUserService
    {
        UserDto Create(string login, string name, string password);
        UserDto FindById(Guid userId);
        UserDto FindByLogin(string login);
        UserDto FindByLoginAndPassword(string login, string password);
        IEnumerable<UserDto> GetUsers(params Guid[] userIds);
    }
}
