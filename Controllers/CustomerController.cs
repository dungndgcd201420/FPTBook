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
using System.Reflection;
using System.Threading.Tasks;
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
          BookId = book.BookId,
          Title = book.Title,
          Author = book.Author,
          Price = book.Price,
          Genre = book.Genre,
          BookStatus = book.BookStatus,
          ImageUrl = image
        };
        bookList.Add(newBook);
      }

      return View(bookList.AsEnumerable());
    }

        public async Task<IActionResult> AddToCart(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
             var bookInStore = _context.Books.SingleOrDefault(t => t.BookId == id);

          var bookInCart = _context.Carts.SingleOrDefault(
              t => t.UserId == currentUserId &&
              t.BookId == id);

        if (bookInCart == null)
        {
        var cartItem = new Cart()
        {
          BookId = id,
          UserId = currentUserId,
          Quantity = 1
          };
          _context.Add(cartItem);
           await _context.SaveChangesAsync();
        }
        else
        {
          bookInCart.Quantity += 1;
        }
      
         return RedirectToAction("Index");
          }
      }
    

    
}
