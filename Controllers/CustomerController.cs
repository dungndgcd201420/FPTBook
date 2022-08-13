using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using Microsoft.AspNetCore.Authorization;
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
    public CustomerController(ApplicationDbContext context)
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
    }

    
}
