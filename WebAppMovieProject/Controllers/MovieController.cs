﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMovieProject.models;

namespace WebAppMovieProject.Controllers
{
    public class MovieController : Controller
    {
        DbmovieappContext db = new DbmovieappContext();
        // GET: MovieController
        public ActionResult Index(int? CategoryId, int? PlatformId)
        {
            
            var movies = db.Movies
                .Include(m => m.Platform)
                .Include(m => m.Categories)
                .AsQueryable();

            if (CategoryId.HasValue && CategoryId.Value > 0)
            {
                movies = movies.Where(m => m.Categories.Any(c => c.CategoryId == CategoryId));
            }
            if(PlatformId.HasValue && PlatformId.Value >0)
            {
                movies = movies.Where(m => m.PlatformId == PlatformId);
            }

            var categories = db.Categories.ToList();
            var platforms = db.Streamingplatforms.ToList();
            ViewBag.Platform = platforms;
            ViewBag.Categories = categories;


            return View(movies.ToList());
        }

        // GET: MovieController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // GET: MovieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: MovieController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MovieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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