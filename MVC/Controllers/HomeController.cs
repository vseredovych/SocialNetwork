using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.DatabaseConfig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVC.Models;
//using MVC.Models.DatabaseConfig;


namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMongoConfiguration _config;
        private readonly IMongoContext _context;
        private readonly IUnitOfWork _unitOfWork;
        //private IMongoContext dbContext = new MongoContext();

        public HomeController(
            ILogger<HomeController> logger, 
            IMongoConfiguration config,
            IMongoContext context,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _config = config;
            _context = context;
            _unitOfWork = unitOfWork;
            
            var mongoConfig = new MongoConfiguration();
            var a = context.Posts();
            var b = _unitOfWork.PostsRepository.GetAll();
            //config.GetSection("MongoDB").Bind(mongoConfig);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
