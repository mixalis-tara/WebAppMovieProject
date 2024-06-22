using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using WebAppMovieProject.Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace WebAppMovieProject.Controllers
{
    public class MovieController : Controller
    {
        dbMovieAppContext db = new dbMovieAppContext();
        // GET: MovieController
        public ActionResult Index(int? CategoryId, int? PlatformId, decimal? ImdbRating)
        {
            
            var movies = db.Movies
                .Include(m => m.Platform)
                .Include(c => c.Categories)
                .OrderBy(x => x.Title)
                .AsQueryable();

            if (CategoryId.HasValue && CategoryId.Value > 0)
            {
                movies = movies.Where(m => m.Categories.Any(c => c.CategoryId == CategoryId)).OrderBy(x => x.Title);
            }
            if(PlatformId.HasValue && PlatformId.Value >0)
            {
                movies = movies.Where(m => m.PlatformId == PlatformId).OrderBy(c => c.Title);
            }

            if (ImdbRating.HasValue)
            {
                movies = movies.Where(m => m.ImdbRating >= ImdbRating).OrderBy(c=>c.ImdbRating);
            }

            var imdbRatings = db.Movies.Select(m => m.ImdbRating).Distinct().OrderBy(r => r).ToList();

            var categories = db.Categories.ToList().OrderBy(c=> c.CategoryName);
            var platforms = db.StreamingPlatforms.ToList().OrderBy(c=> c.PlatformName);

            ViewBag.Platform = platforms;
            ViewBag.Categories = categories;
            ViewBag.ImdbRatings = imdbRatings;


            return View(movies.ToList());
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            var movie = db.Movies
                        .Include(m => m.Platform)
                        .Include(m => m.Categories)
                        .FirstOrDefault(c => c.MovieId == id);
            
            if(movie == null)
            {
                return NotFound();
            }
            
            
            return View(movie);
        }



        // GET: MovieController/Create
        public ActionResult Create()
        {
            var categories = db.Categories
                .OrderBy(c => c.CategoryName)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

            var platforms = db.StreamingPlatforms
                .OrderBy(c => c.PlatformName)
                .Select(c => new SelectListItem
                {
                    Value = c.PlatformId.ToString(),
                    Text = c.PlatformName
                }).ToList();

            var viewModel = new MovieViewModel
            {
                Categories = categories,
                Platforms = platforms
            };
            return View(viewModel);
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieViewModel viewModel)
        {

            try
            {
                viewModel.Movie.Categories = db.Categories
                        .Where(c => viewModel.SelectedCategoryIds.Contains(c.CategoryId))
                        .ToList();

                
                db.Movies.Add(viewModel.Movie);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch
            {

            }
            

            return View();
        }

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0 )
            {
                return NotFound();
            }
            var movie = db.Movies
                        .Include(m => m.Categories)
                        .FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            var categories = db.Categories
                .OrderBy(c => c.CategoryName)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

            var platforms = db.StreamingPlatforms
                .OrderBy(c => c.PlatformName)
                .Select(c => new SelectListItem
                {
                    Value = c.PlatformId.ToString(),
                    Text = c.PlatformName
                }).ToList();

            var viewModel = new MovieViewModel
            {
                Movie = movie,
                SelectedCategoryIds = movie.Categories.Select(c => c.CategoryId).ToArray(),
                Categories = categories,
                Platforms = platforms
            };
            return View(viewModel);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieViewModel viewModel)
        {
            try
            {
                var movieToUpdate = db.Movies
                .Include(m => m.Categories)
                .FirstOrDefault(m => m.MovieId == id);
                viewModel.Movie.Categories = db.Categories
                        .Where(c => viewModel.SelectedCategoryIds.Contains(c.CategoryId))
                        .ToList();


                db.Entry(movieToUpdate).CurrentValues.SetValues(viewModel.Movie);
                movieToUpdate.Categories.Clear();
                foreach (var categoryId in viewModel.SelectedCategoryIds)
                {
                    var category = db.Categories.Find(categoryId);
                    if (category != null)
                    {
                        movieToUpdate.Categories.Add(category);
                    }
                }
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MovieController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
