using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FPTBook.Controllers
{
  [Authorize(Roles = Role.CUSTOMER)]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CustomerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
          _context = context;
          _userManager = userManager;
        }

        public IActionResult Index()
        {
          IEnumerable<Book> books = _context.Books
            .Include(t => t.Genre)
            .ToList();
          return View(books);
        }
        
        [HttpGet]
        public IActionResult AddToCart(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var bookInDb = _context.Books.SingleOrDefault(t => t.BookId == id);
            var cartItem = new Cart()
            {
                UserId = currentUserId,
                Book = bookInDb,
                Quantity = 1
            };
            var order = new Order();
            order.CartItems = new List<Cart>();
            order.CartItems.Add(cartItem);

            return View();
        }
    }

    
}
