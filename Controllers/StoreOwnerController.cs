using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Controllers
{
  [Authorize]
  public class StoreOwnerController : Controller
    {
    private ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public StoreOwnerController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }
    public IActionResult Index(string genre)
    {
      var currentUserId = _userManager.GetUserId(User);


      if (!string.IsNullOrWhiteSpace(genre))
      {
        var result = _context.Books
          .Include(t => t.Genre)
          .Where(t => t.Genre.Description.Equals(genre)
             && t.UserId == currentUserId)
          .ToList();

        return View(result);
      }
        IEnumerable<Book> books = _context.Books
       .Include(t => t.Genre)
       .Where(t => t.Genre.Description.Equals(genre)
             && t.UserId == currentUserId)
        .ToList();
        return View(books);

      }
    [HttpGet]
    public IActionResult Insert()
    {
      var currentUserId = _userManager.GetUserId(User);
      var viewModel = new BookGenreViewModel()
      {
        Genres = _context.Genres.ToList()
         
      };
      return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(BookGenreViewModel viewModel)
    {
      if (!ModelState.IsValid)
      {
        viewModel = new BookGenreViewModel
        {
          Genres = _context.Genres.ToList()
        };
        return View(viewModel);
      }
      var newBook = new Book
      {
        Title = viewModel.Book.Title,
        Price = viewModel.Book.Price,
        Description = viewModel.Book.Description,
        GenreId = viewModel.Book.GenreId
      };
      _context.Add(newBook);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
      var todoInDb = _context.Books.SingleOrDefault(t => t.BookId == id);
      if (todoInDb is null)
      {
        return NotFound();
      }

      var viewModel = new BookGenreViewModel()
      {
        Book = todoInDb,
        Genres = _context.Genres.ToList()
      };
      return View(viewModel);
    }
    [HttpPost]
    public IActionResult Update(BookGenreViewModel viewModel)
    {
      var todoInDb = _context.Books.SingleOrDefault(t => t.BookId == viewModel.Book.BookId);
      if (todoInDb is null)
      {
        return BadRequest();
      }
      if (!ModelState.IsValid)
      {
        viewModel = new BookGenreViewModel()
        {
          Book = viewModel.Book,
          Genres = _context.Genres.ToList()
        };
        return View(viewModel);
      }
      todoInDb.Title = viewModel.Book.Title;
      todoInDb.Description = viewModel.Book.Description;
      todoInDb.BookStatus = viewModel.Book.BookStatus;
      todoInDb.Price = viewModel.Book.Price;
      todoInDb.GenreId = viewModel.Book.GenreId;

      _context.SaveChanges();

      return RedirectToAction("Index");
    }
  }

 
}

