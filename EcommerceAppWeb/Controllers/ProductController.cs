using EcommerceAppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAppWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly EcommerceAppWebContext db;

        public ProductController(EcommerceAppWebContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var products = db.Products.Include(p => p.Category);
            return View(products.ToList());
        }

        public IActionResult Details(int? id)
        {

            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);

        }
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Product p)
        {
            p.LastUpdated = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", p.CategoryId);
            return View(p);
        }

        public IActionResult Edit(int? id)
        {
            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Product p)
        {
            p.LastUpdated = DateTime.Now;
            db.Products.Update(p);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int? id)
        {
            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            db.Products.Remove(product);
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
