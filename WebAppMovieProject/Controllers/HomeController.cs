using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebAppMovieProject.Models;

namespace WebAppMovieProject.Controllers
{
    public class HomeController : Controller
    {
        dbMovieAppContext db = new dbMovieAppContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            
            var movieList = db.Movies
                .Include(m => m.Platform)
                .Include(x => x.Categories)
                .Where(c => c.Watched == false)
                .ToList();
            return View(movieList);
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
