using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maximov.SunMessenger.Web.Models.Shared.User;
using Maximov.SunMessenger.Web.Models.User;
using Maximov.SunMessenger.Web.Services.AuthenticationService.Abstractions;
using Maxinov.SunMessenger.Services.DTO;
using Maxinov.SunMessenger.Services.UserService.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Maximov.SunMessenger.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IAuthenticationService authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenicationService)
        {
            this.userService = userService;
            this.authenticationService = authenicationService;
        }

        #region Login/Registration/Logout

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var res = await authenticationService.AuthenticateByLoginAndPassword(model.Login, model.Password);
                if(res.IsSuccess)
                {
                    return RedirectToAction("Index", "Chat");
                }
                ModelState.AddModelError("", res.Error);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Registration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDto user = userService.Create(model.Login, model.Name, model.Password);
                authenticationService.AuthenticateByLoginAndPassword(user.Login, user.Password);
                return RedirectToAction("Index", "Chat");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            authenticationService.Logout();
            return RedirectToAction("Login");
        }

        #endregion

        public JsonResult Find(Guid userId)
        {
            Guid currentUserId = authenticationService.GetUserId();
            UserViewModel userInfo = null;

            var user = userService.FindById(userId);
            if (user != null)
                userInfo = new UserViewModel()
                {
                    Id=user.Id,
                    Login = user.Login,
                    Name = user.Name,
                    IsCurrentUser=user.Id==currentUserId
                };

            return Json(userInfo);
        }
    }
}
