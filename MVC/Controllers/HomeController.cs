using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC.Models;
using MVC.Models.Services;
using MVC.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;
//using MVC.Models.DatabaseConfig;

namespace MVC.Controllers
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

        public async Task<IActionResult> Index()
        {
            PostIndexViewModel vm = new PostIndexViewModel()
            {
                PostItems = await _postService.GetAllAsync()
            };

            return View(vm);
        }

        //public IActionResult LikePost(string id)
        //{
        //    _postService.LikePost(id);
        //    return View(_postService);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LikePost(PostItemViewModel model)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    _postService.LikePost(dto._id);
            //}
            return View(_postService);
        }
        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
