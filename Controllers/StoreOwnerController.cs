using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
      IEnumerable<Book> books = _context.Books.ToList();
      return View(books);
    }
    }
}
