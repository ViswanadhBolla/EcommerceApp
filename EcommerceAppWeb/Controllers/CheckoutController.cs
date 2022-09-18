using System.Diagnostics.Eventing.Reader;
using EcommerceAppWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAppWeb.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly EcommerceAppWebContext db;
        
        public CheckoutController(EcommerceAppWebContext db)
        {
            this.db = db;
        }

        public IActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddressAndPayment(CustomerOrder order)
        {
            var context = HttpContext;
            order.CustomerUserName = context.User.Identity.Name;
            order.DateCreated = DateTime.Now;
            db.CustomerOrders.Add(order);
            db.SaveChanges();
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.CreateOrder(order);
            db.SaveChanges();
            return RedirectToAction("Complete", new {id = order.Id});
        }
       
        public IActionResult Complete(int id)
        {
            var context = HttpContext;
            bool isvalid = db.CustomerOrders.Any(
                o => o.Id == id &&
                o.CustomerUserName == context.User.Identity.Name);
            if (isvalid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}
