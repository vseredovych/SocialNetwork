using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Core.Entities;
using MVC.Interfaces;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public HomeController(
            ILogger<HomeController> logger,
            IPostService postService,
            IUserService userService)
        {
            _logger = logger;
            _postService = postService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _postService.GetAllAsync();

            return View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> LikePost(string postId)
        {
            var post = await _postService.IncPostLikesAsync(postId);
            return NoContent();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(string postText)
        {
            //var post = await _postService.LikePostAsync(commentText);
            var viewModel = await _postService.GetAllAsync();
            var user = await _userService.GetByEmailAsync(User.Identity.Name);
            var newel = new Post()
            {
                AuthorEmail = user.Email,
                AuthorName = user.Name,
                AuthorSurname = user.Surname,
                Text = postText,
                Likes = 0,
                Timestamp = DateTime.Now,
                Comments = new List<Comment>()
            };

            _postService.InsertPostAsync(newel);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> AddComment(string commentText, string postId)
        {
            //var post = await _postService.LikePostAsync(commentText);
            var viewModel = await _postService.GetAllAsync();
            var user = await _userService.GetByEmailAsync(User.Identity.Name);
            var newel = new Comment()
            {
                AuthorEmail = user.Email,
                AuthorName = user.Name,
                AuthorSurname = user.Surname,
                Text = commentText,
                Likes = 0,
                Timestamp = DateTime.Now
            };

            _postService.InsertCommentAsync(newel, postId);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
