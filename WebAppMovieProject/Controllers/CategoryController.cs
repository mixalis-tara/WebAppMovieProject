using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMovieProject.Models;

namespace WebAppMovieProject.Controllers
{
    public class CategoryController : Controller
    {
        dbMovieAppContext db = new dbMovieAppContext();
        // GET: CategoryController
        public ActionResult Index()
        {
            var categoryList = db.Categories.ToList();
            return View(categoryList);
        }



        // GET: CategoryController/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category theCategory)
        {
            try
            {
                db.Categories.Add(theCategory);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category theCategory)
        {
            try
            {
                db.Update(theCategory);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                if (category == null)
                {
                    return NotFound();
                }

                db.Categories.Remove(category);
                db.SaveChanges();

                TempData["Message"] = "Category deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                TempData["ErrorMessage"] = $"Error deleting category: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
