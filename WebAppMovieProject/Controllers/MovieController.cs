using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using WebAppMovieProject.Models;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace WebAppMovieProject.Controllers
{
    public class MovieController : Controller
    {
        private readonly dbMovieAppContext db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly HttpClient _httpClient;
        public MovieController(dbMovieAppContext dbase, IWebHostEnvironment hostEnvironment, HttpClient httpClient)
        {
            db = dbase;
            _hostEnvironment = hostEnvironment;
            _httpClient = httpClient;
        }
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
            if (PlatformId.HasValue && PlatformId.Value > 0)
            {
                movies = movies.Where(m => m.PlatformId == PlatformId).OrderBy(c => c.Title);
            }

            if (ImdbRating.HasValue)
            {
                movies = movies.Where(m => m.ImdbRating >= ImdbRating).OrderBy(c => c.ImdbRating);
            }

            var imdbRatings = db.Movies.Select(m => m.ImdbRating).Distinct().OrderBy(r => r).ToList();

            var categories = db.Categories.ToList().OrderBy(c => c.CategoryName);
            var platforms = db.StreamingPlatforms.ToList().OrderBy(c => c.PlatformName);

            ViewBag.Platform = platforms;
            ViewBag.Categories = categories;
            ViewBag.ImdbRatings = imdbRatings;


            return View(movies.ToList());
        }

        // GET: MovieController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var movie = db.Movies
                        .Include(m => m.Platform)
                        .Include(m => m.Categories)
                        .Include(c => c.Actors)
                        .FirstOrDefault(c => c.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            string synopsis = await GetSynopsisByTitle(movie.Title);
            ViewBag.Synopsis = synopsis;
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

            var actors = db.Actors
                .OrderBy(c => c.ActorName)
                .Select(c => new SelectListItem
                {
                    Value = c.ActorId.ToString(),
                    Text = c.ActorName
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
                Platforms = platforms,
                Actors = actors
            };
            return View(viewModel);
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel viewModel)
        {

            try
            {
                if (viewModel.ImageFile != null && viewModel.ImageFile.Length > 0)
                {
                    viewModel.Movie.ImageUrl = await UploadImageToImgBB(viewModel.ImageFile);
                }

                viewModel.Movie.Categories = db.Categories
                        .Where(c => viewModel.SelectedCategoryIds.Contains(c.CategoryId))
                        .ToList();

                viewModel.Movie.Actors = db.Actors
                        .Where(c => viewModel.SelectedActorsIds.Contains(c.ActorId))
                        .ToList();


                db.Movies.Add(viewModel.Movie);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // Handle exception
                ModelState.AddModelError(string.Empty, "Error occurred while saving.");
            }


            return View();
        }
        

        // GET: MovieController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var movie = db.Movies
                        .Include(m => m.Categories)
                        .Include(c => c.Actors)
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

            var actors = db.Actors
                .OrderBy(c => c.ActorName)
                .Select(c => new SelectListItem
                {
                    Value = c.ActorId.ToString(),
                    Text = c.ActorName
                }).ToList();

            var viewModel = new MovieViewModel
            {
                Movie = movie,
                SelectedCategoryIds = movie.Categories.Select(c => c.CategoryId).ToArray(),
                Categories = categories,
                SelectedActorsIds = movie.Actors.Select(c => c.ActorId).ToArray(),
                Actors = actors,
                Platforms = platforms,
                CurrentImageName = movie.ImageUrl
            };
            
            return View(viewModel);
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieViewModel viewModel, IFormFile imageFile)
        {
            try
            {
                var movieToUpdate = db.Movies
                .Include(m => m.Categories)
                .Include(c => c.Actors)
                .FirstOrDefault(m => m.MovieId == id);


                viewModel.Movie.Categories = db.Categories
                        .Where(c => viewModel.SelectedCategoryIds.Contains(c.CategoryId))
                        .ToList();
                viewModel.Movie.Actors = db.Actors
                        .Where(c => viewModel.SelectedActorsIds.Contains(c.ActorId))
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
                movieToUpdate.Actors.Clear();
                foreach (var actorId in viewModel.SelectedActorsIds)
                {
                    var actor = db.Actors.Find(actorId);
                    if (actor != null)
                    {
                        movieToUpdate.Actors.Add(actor);
                    }
                }
                if (imageFile != null && imageFile.Length > 0)
                {
                    movieToUpdate.ImageUrl = await UploadImageToImgBB(imageFile);
                }
                else
                {
                    movieToUpdate.ImageUrl = viewModel.CurrentImageName;
                }



                db.Update(movieToUpdate);
                await db.SaveChangesAsync();

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
            var movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: MovieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Movie theMovie)
        {
            try
            {
                var movie = db.Movies.Find(id);
                if (movie == null)
                {
                    return NotFound();
                }
                db.Movies.Remove(movie);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> UploadImageToImgBB(IFormFile imageFile)
        {
            try
            {
                _httpClient.BaseAddress = new Uri("https://api.imgbb.com/1/");
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent("96a5f4bf06400ad6edb3b81b9c706a80"), "key");
                formData.Add(new StreamContent(imageFile.OpenReadStream()), "image", imageFile.FileName);

                var response = await _httpClient.PostAsync("upload", formData);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);
                    string imageUrl = jsonResponse.data.url;
                    return imageUrl;
                }
                else
                {

                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to upload image to imgBB. Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error uploading image to imgBB", ex);
            }
        }
        private async Task<string> GetTconstByTitle(string title)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/find?q={Uri.EscapeDataString(title)}"),
                Headers =
                {
                    { "x-rapidapi-key", "2fbf2bb0f0msh105344e2de9a71ep111cb3jsn3f45f1539954" },
                    { "x-rapidapi-host", "imdb8.p.rapidapi.com" },
                },
            };

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(body);
                var movie = result.results[0]; 
                return movie.id;
            }
        }

        private async Task<string> GetSynopsisByTconst(string tconst)
        {
            tconst = tconst.Replace("/title/", "").Trim('/');

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/get-synopses?tconst={tconst}"),
                Headers =
                {
                    { "x-rapidapi-key", "2fbf2bb0f0msh105344e2de9a71ep111cb3jsn3f45f1539954" },
                    { "x-rapidapi-host", "imdb8.p.rapidapi.com" },
                },
                    };

            using (var response = await _httpClient.SendAsync(request))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Request failed with status code {response.StatusCode}. Error: {errorContent}");
                }

                var body = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(body))
                {
                    throw new Exception("Response content is empty.");
                }

                dynamic result = JsonConvert.DeserializeObject(body);

                if (result == null || result.Count == 0)
                {
                    throw new Exception("No synopsis found in the response.");
                }

                return result[0].text;
            }
        }

        private async Task<string> GetSynopsisByTitle(string title)
        {
            string tconst = await GetTconstByTitle(title);
            return await GetSynopsisByTconst(tconst);
        }


    }
}
