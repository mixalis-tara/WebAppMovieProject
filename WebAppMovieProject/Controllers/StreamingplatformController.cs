using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppMovieProject.Models;

namespace WebAppMovieProject.Controllers
{
    public class StreamingplatformController : Controller
    {
        dbMovieAppContext db = new dbMovieAppContext();
        // GET: StreamingplatformController
        public ActionResult Index()
        {
            
            var streamPlat = db.StreamingPlatforms.OrderBy(c=>c.PlatformName).ToList();
            return View(streamPlat);
        }


        // GET: StreamingplatformController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StreamingplatformController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StreamingPlatform theStreamingPlatform)
        {
            try
            {
                db.StreamingPlatforms.Add(theStreamingPlatform);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StreamingplatformController/Edit/5
        public ActionResult Edit(int id)
        {
            var streamPlat = db.StreamingPlatforms.Find(id);
            if(streamPlat == null)
            {
                return NotFound();
            }
            return View(streamPlat);
        }

        // POST: StreamingplatformController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StreamingPlatform theStreamingPlatform)
        {
            try
            {
                db.Update(theStreamingPlatform);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StreamingplatformController/Delete/5
        public ActionResult Delete(int id)
        {
            var streamPlat = db.StreamingPlatforms.Find(id);
            if (streamPlat == null)
            {
                return NotFound();
            }
            return View(streamPlat);
        }

        // POST: StreamingplatformController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var streamPlat = db.StreamingPlatforms.Find(id);
                if (streamPlat == null)
                {
                    return NotFound();
                }

                db.StreamingPlatforms.Remove(streamPlat);
                db.SaveChanges();

                TempData["Message"] = "Platform deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = $"Error deleting platform: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
