using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Core.Entities;
using MVC.Web.Interfaces;
using MVC.Web.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
//using MVC.Models.DatabaseConfig;

namespace MVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        //private IMongoContext dbContext = new MongoContext();

        public HomeController(ILogger<HomeController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var viewModel = await _postService.GetAllAsync();

            return View(viewModel);
        }

        //public IActionResult LikePost(string id)
        //{
        //    _postService.LikePost(id);
        //    return View(_postService);
        //}

        public async Task<IActionResult> LikePost(string postId)
        {
            var post = await _postService.IncPostLikesAsync(postId);
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string commentText)
        {
            //var post = await _postService.LikePostAsync(commentText);
            var viewModel = await _postService.GetAllAsync();
            var newel = new Post()
            {
                AuthorEmail = "A",
                AuthorName = "AA",
                AuthorSurname = "AAA",
                Text = commentText,
                Likes = 0,
                Timestamp = DateTime.Now
            };

            //foreach (var el in viewModel)
            //{
            //    var new_el = el;
            //    new_el.Text = commentText;
            //    _postService.InsertPost(new_el);
            //}
            _postService.InsertPostAsync(newel);

            return NoContent();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
