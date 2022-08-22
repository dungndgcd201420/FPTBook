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
using System.Security.Claims;
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
        public IActionResult Index(string word)
        {
            if (!string.IsNullOrWhiteSpace(word))
            {
                var result = _context.Books
                  .Include(t => t.Genre)
                  .Where(t => t.Title.Contains(word) || t.Genre.Description.Contains(word))
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
                    BookStatus = Enums.BookStatus.inStock,
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
    public async Task<IActionResult> Update(BookGenreViewModel viewModel)
    {

      using (var memoryStream = new MemoryStream())
      {
        //If there is file
        if (viewModel.FormFile != null)
        {
          await viewModel.FormFile.CopyToAsync(memoryStream);
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
          bookInDb.ImageData = memoryStream.ToArray();
          _context.SaveChanges();
        }
        else //If there is no file
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
        }
      }
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
        [HttpGet]
        public IActionResult GenreList()
        {
            var genres = _context.Genres.Include(t => t.Books).ToList();

            return View(genres);
        }

        [HttpGet]
        public IActionResult GenreRequest()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenreRequest(int id, string description)
        { 
            Genre genre = new Genre()
            {
                Id = id,
                Description = description,
                Status = Enums.GenreApproval.pending
            };
            _context.Add(genre);

            await _context.SaveChangesAsync();
            return RedirectToAction("GenreList");
        }
        public IActionResult ReadMessage(int id)
        {
            var notiInDb = _context.Notifications.SingleOrDefault(t => t.NotiId == id);
            if (notiInDb is null)
            {
                return BadRequest();
            }

            notiInDb.NotiStatus = Enums.NotiCheck.seen;

            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> OrderList()
        {
            var orderList = _context.Orders.Include(t=> t.CartList).ToList();

            await _context.SaveChangesAsync();
            return View(orderList.AsEnumerable());
        }
    }
}

 


