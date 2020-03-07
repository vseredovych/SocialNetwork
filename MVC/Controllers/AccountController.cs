using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Interfaces;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public AccountController(
            ILogger<AccountController> logger,
            IPostService postService,
            IUserService userService
            )
        {
            _logger = logger;
            _postService = postService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var authorizedUser = await _userService.GetByEmailAsync(User.Identity.Name);
            var profileModel = await _userService.GetProfileModel(authorizedUser);
            profileModel.IsAuthorized = true;

            return View(profileModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ProfileByEmail(string authorEmail)
        {
            var authorizedUser = await _userService.GetByEmailAsync(User.Identity.Name);

            var userModel = await _userService.GetByEmailAsync(authorEmail);
            var profileModel = await _userService.GetProfileModel(userModel);

            if (authorizedUser.Email == profileModel.Email)
            {
                profileModel.IsAuthorized = true;
            }
            return View("Profile", profileModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userProfile = await _userService.UpdateUserByEmailAsync(viewModel);
                return RedirectToAction("Profile", "Account");
            }
            ModelState.AddModelError("", "Wrong Password or Login");
            return NoContent();
        }

        #region Authentication
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isAuthorized = await _userService
                    .CheckPasswordByEmailAsync(model.Email, model.Password);

                if (isAuthorized)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong Password or Login");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isUserExists = await _userService.IsUserExistsAsync(model.Email);
                if (!isUserExists)
                {
                    if (model.Password == model.ConfirmPassword)
                    {
                        var newUser = new UserViewModel()
                        {
                            Email = model.Email,
                            HashPassword = model.Password,
                            Name = "Anonym",
                            Surname = "Anonymus"
                        };
                        _userService.InsertUserAsync(newUser);
                    }
                    return RedirectToAction("Profile", "Account");
                }
                else
                    ModelState.AddModelError("", "Wrong Password or Login");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        #endregion
    }
}