using Maximov.SunMessenger.Services.DTO;
using OperationResult;
using System;
using System.Threading.Tasks;

namespace Maximov.SunMessenger.Web.Services.AuthenticationService.Abstractions
{
    public interface IAuthenticationService
    {
        Task<Status<string>> AuthenticateByLoginAndPassword(string login, string password);
        void Logout();
        Guid GetUserId();
    }
}