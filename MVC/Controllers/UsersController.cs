using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public UsersController(
            ILogger<UsersController> logger,
            IPostService postService,
            IUserService userService
            )
        {
            _logger = logger;
            _postService = postService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("Users", "Users");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddFriend(string userEmail)
        {
            ViewData["authorizedUser"] = User.Identity.Name;
            _userService.AddFriend(User.Identity.Name, userEmail);
            return RedirectToAction("Users", "Users");
        }
        [HttpPost]
        [Authorize]
        public IActionResult RemoveFriend(string userEmail)
        {
            ViewData["authorizedUser"] = User.Identity.Name;
            _userService.RemoveFriend(User.Identity.Name, userEmail);
            return RedirectToAction("Users", "Users");
        }
    }
}
