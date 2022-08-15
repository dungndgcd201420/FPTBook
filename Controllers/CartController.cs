using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace FPTBook.Controllers
{
  [Authorize(Roles = Role.CUSTOMER)]
  public class CartController : Controller
  {
    private ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public IActionResult Index()
    {
      var currentUserId = _userManager.GetUserId(User);

      IEnumerable<Cart> booksInCart = _context.Carts
            .Include(t=> t.Book)
            .Where(t => t.UserId == currentUserId)
            .ToList();

      return View(booksInCart);
    }
  
  }
  }

