using EcommerceAppWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAppWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly EcommerceAppWebContext db;

        public CategoryController(EcommerceAppWebContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public IActionResult Details(int? id)
        {
            Category category = db.Categories.SingleOrDefault(p => p.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category c)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        public IActionResult Edit(int? id)
        {
            Category category = db.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category c)
        {
            if (ModelState.IsValid)
            {
                db.Update(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(c);
        }

        public IActionResult Delete(int? id)
        {
            Category category = db.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.SingleOrDefault(c => c.Id == id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
