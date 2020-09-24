using Maximov.SunMessenger.Web.Services.AuthenticationService.Abstractions;
using Maximov.SunMessenger.Services.DTO;
using Maximov.SunMessenger.Services.UserService;
using Maximov.SunMessenger.Services.UserService.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static OperationResult.Helpers;

namespace Maximov.SunMessenger.Web.Services.AuthenticationService
{
    public class AuthenticationService : Abstractions.IAuthenticationService
    {
        private readonly HttpContext httpContext;
        private readonly Maximov.SunMessenger.Services.UserService.Abstractions.IUserService userService;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.userService = userService;
        }



        public async Task<Status<string>> AuthenticateByLoginAndPassword(string login, string password)
        {
            var user = userService.FindByLoginAndPassword(login, password);
            if (user != null)
            {
                await Authenticate(user);
                return Ok();
            }
            return Error("Неверный логин и/или пароль");
        }

        public async void Logout()
        {
            await httpContext.SignOutAsync();
        }

        public Guid GetUserId()
        {
            string guidString = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid guid = new Guid(guidString);
            return guid;
        }


        private async Task Authenticate(UserDto userDto)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await httpContext.SignInAsync(claimsPrincipal);
        }
    }
}
