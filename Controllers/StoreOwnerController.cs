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
  }
}
