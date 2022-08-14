using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace FPTBook.Controllers
{
  [Authorize(Roles = Role.CUSTOMER)]
  public class CustomerController : Controller
  {
    private ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public CustomerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public IActionResult Index()
    {
      var bookList = new List<BookDisplayViewModel>();
      if (bookList is null)
      {
        return NotFound();
      }
      IEnumerable<Book> books = _context.Books
          .Include(t => t.Genre)
          .ToList();

      foreach (Book book in books)
      {
        string imageBase64Data = Convert.ToBase64String(book.ImageData);
        string image = string.Format("data:image/jpg;base64, {0}", imageBase64Data);

        var newBook = new BookDisplayViewModel()
        {
          Title = book.Title,
          Author = book.Author,
          Price = book.Price,
          Genre = book.Genre,
          BookStatus = book.BookStatus,
          ImageUrl = image
        };
        bookList.Add(newBook);
      }
      IEnumBookList = bookList.AsEnumerable();

      return View(IEnumBookList);
    }
    public IEnumerable<BookDisplayViewModel> IEnumBookList { get; set; }

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
