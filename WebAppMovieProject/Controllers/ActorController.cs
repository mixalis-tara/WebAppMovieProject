using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppMovieProject.Models;

namespace WebAppMovieProject.Controllers
{
    public class ActorController : Controller
    {
        dbMovieAppContext db = new dbMovieAppContext();
        // GET: ActorController

        public ActionResult Index()
        {
            var actorList = db.Actors.ToList();
            return View(actorList);
        }


        // GET: ActorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor theActor)
        {
            try
            {
                db.Actors.Add(theActor);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActorController/Edit/5
        public ActionResult Edit(int id)
        {
            var actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: ActorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Actor theActor)
        {
            try
            {   
                db.Update(theActor);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ActorController/Delete/5
        public ActionResult Delete(int id)
        {
            var actor = db.Actors.Find(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: ActorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var actor = db.Actors.Find(id);
                if (actor == null)
                {
                    return NotFound();
                }

                db.Actors.Remove(actor);
                db.SaveChanges();

                TempData["Message"] = "Actor deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = $"Error deleting actor: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
