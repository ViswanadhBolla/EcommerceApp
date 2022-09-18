using System.Text.Encodings.Web;
using EcommerceAppWeb.Models;
using EcommerceAppWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAppWeb.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly EcommerceAppWebContext db;

        public ShoppingCartController(EcommerceAppWebContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var viewmodel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()

            };
            return View(viewmodel);
        }

        public IActionResult AddToCart(int id)
        {
            var addedProduct = db.Products.Single(product=>product.Id == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string productName = db.Carts.FirstOrDefault(item => item.ProductId == id).Product.Name;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message =  HtmlEncoder.Default.Encode(productName) + " has been removed from your shopping cart",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }
}
