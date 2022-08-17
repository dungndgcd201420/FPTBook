using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FPTBook.Controllers
{
  public class OrderController : Controller
  {
    private ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public IActionResult Index()
    {
      var currentUserId = _userManager.GetUserId(User);
      IEnumerable<Order> order = _context.Orders
       .Include(t => t.CartList)
       .Where(t=> t.UserId == currentUserId)
       .ToList();
      return View(order);
    }

  }
}
