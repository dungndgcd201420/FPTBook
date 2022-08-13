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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Controllers
{
  [Authorize(Roles = Role.OWNER)]
  public class StoreOwnerController : Controller
    {
        private ApplicationDbContext _context;
        public StoreOwnerController(ApplicationDbContext context)
        {
          _context = context;
        }
        public IActionResult Index(string genre)
        {


          if (!string.IsNullOrWhiteSpace(genre))
          {
            var result = _context.Books
              .Include(t => t.Genre)
              .Where(t => t.Genre.Description.Equals(genre))
              .ToList();

            return View(result);
          }
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
      using (var memoryStream = new MemoryStream())
      {
        await viewModel.FormFile.CopyToAsync(memoryStream);
        var newBook = new Book
        {
          Title = viewModel.Book.Title,
          Price = viewModel.Book.Price,
          Author = viewModel.Book.Author,
          GenreId = viewModel.Book.GenreId,
          ImageData = memoryStream.ToArray()
        };
        _context.Add(newBook);
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
      var bookInDb = _context.Books.SingleOrDefault(t => t.BookId == id);
      if (bookInDb is null)
      {
        return NotFound();
      }

      var viewModel = new BookGenreViewModel()
      {
        Book = bookInDb,
        Genres = _context.Genres.ToList()
      };
      return View(viewModel);
    }
    [HttpPost]
    public IActionResult Update(BookGenreViewModel viewModel)
    {
      var bookInDb = _context.Books.SingleOrDefault(t => t.BookId == viewModel.Book.BookId);
      if (bookInDb is null)
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
        bookInDb.Title = viewModel.Book.Title;
        bookInDb.Author = viewModel.Book.Author;
        bookInDb.BookStatus = viewModel.Book.BookStatus;
        bookInDb.Price = viewModel.Book.Price;
        bookInDb.GenreId = viewModel.Book.GenreId;

          _context.SaveChanges();

          return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var bookInDb = _context.Books
            .Include(t => t.Genre)
            .SingleOrDefault(t => t.BookId == id)
              ;
            if (bookInDb is null)
            {
                return NotFound();
            }
           string imageBase64Data = Convert.ToBase64String(bookInDb.ImageData);
           string image = string.Format("data:image/jpg;base64, {0}", imageBase64Data);
           ViewBag.ImageData = image;


           return View(bookInDb);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(t => t.BookId == id);
            if (bookInDb is null)
            {
                return NotFound();
            }
     
            _context.Books.Remove(bookInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

 
}

