using EcommerceAppWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAppWeb.Controllers
{
    public class StoreFrontController : Controller
    {
        private readonly EcommerceAppWebContext db;

        public StoreFrontController(EcommerceAppWebContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int? id)
        {
            var result = db.Categories.Include(x=>x.Products);
            Category category = result.SingleOrDefault(c=>c.Id==id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
    }
}
