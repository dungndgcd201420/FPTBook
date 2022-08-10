using FPTBook.Data;
using FPTBook.Models;
using FPTBook.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Controllers
{
    public class StoreOwnerController : Controller
    {
    private ApplicationDbContext _context;
    public StoreOwnerController(ApplicationDbContext context)
    {
      _context = context;
    }
    public IActionResult Index()
        {
      IEnumerable<Book> books = _context.Books
       .Include(t => t.Genre)
        .ToList();
      return View(books);
    }
    [HttpGet]
    public IActionResult Insert()
    {
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
      todoInDb.Status = viewModel.Book.Status;
      todoInDb.Price = viewModel.Book.Price;
      todoInDb.GenreId = viewModel.Book.GenreId;

      _context.SaveChanges();

      return RedirectToAction("Index");
    }
  }

 
}

