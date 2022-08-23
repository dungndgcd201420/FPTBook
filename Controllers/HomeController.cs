using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using FPTBook.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            BookNotificationViewModel bookNoti = new BookNotificationViewModel();
            IEnumerable<Notification> notifications = _context.Notifications.Where(t => t.NotiStatus == Enums.NotiCheck.unSeen).ToList();
            var bookList = new List<BookDisplayViewModel>();
            if (bookList is null)
            {
                return NotFound();
            }

            IEnumerable<Book> books = _context.Books
                 .Include(t => t.Genre)
                 .ToList();
            foreach (var book in books)
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
            bookNoti.BookDisplay = bookList.AsEnumerable();
            bookNoti.Notifications = notifications;
            return View("~/Views/Home/Index.cshtml", bookNoti);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult FAQs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
