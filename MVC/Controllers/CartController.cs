using System.Collections.Generic;
using System.Linq;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using NET104_ASM.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly CompanyContext _context;

        public CartController(CompanyContext context)
        {
            _context = context;
        }


        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Cart");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }

        public IActionResult Index()
        {
            return View(Carts);
        }
        [BlockCartcs]

        [HttpPost]
        public IActionResult AddToCart(int id, int soluong)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.FoodId == id);
            if (item == null)
            {
                var food = _context.Foods.SingleOrDefault(p => p.FoodId == id);
                if (food == null)
                {
                    return NotFound();
                }
                item = new CartItem
                {
                    FoodId = id,
                    NameCart = food.Name,
                    Price = food.Price,
                    Quantity = soluong,
                    ImageCart = food.Image
                };
                myCart.Add(item);
            }
            else
            {
                item.Quantity += soluong;
            }
            HttpContext.Session.Set("Cart", myCart);           
            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Remove(int id)
        {
            var data = HttpContext.Session.Get<List<CartItem>>("Cart");
            if (data != null)
            {
                var itemToRemove = data.FirstOrDefault(p => p.FoodId == id);
                if (itemToRemove != null)
                {
                    data.Remove(itemToRemove);
                    HttpContext.Session.Set("Cart", data);
                }
            }

            return RedirectToAction("Index", "Cart");
        }
        public IActionResult Checkout()
        {
            // Lấy thông tin giỏ hàng từ Session
            var cartItems = HttpContext.Session.Get<List<CartItem>>("Cart");
            // Truyền danh sách sản phẩm trong giỏ hàng đến view
            return View(cartItems);
        }

    }
}
