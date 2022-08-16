using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

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
      IEnumerable<Order>
      return View();
    }
  }
}
