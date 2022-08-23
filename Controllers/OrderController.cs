using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Controllers
{
  [Authorize(Roles = Role.CUSTOMER)]
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
       .Where(t=> t.UserId == currentUserId && t.OrderStatus == Enums.OrderStatus.notPaid)
       .ToList();
      return View(order);
    }
    public async Task<IActionResult> FinishPayment(int id)
    {
      var currentUserId = _userManager.GetUserId(User);
      var order = _context.Orders.SingleOrDefault(t => t.OrderId == id);

      var cartOfCurrentOrder = _context.Carts.Include(t => t.Book).Where(t => t.UserId == currentUserId);
      var cartInDb = _context.Carts;
   
      if (order == null)
      {
        return NotFound();

      }
      order.OrderStatus = Enums.OrderStatus.paid;

      foreach (var book in cartOfCurrentOrder)
      {
        cartInDb.Remove(book);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction("Index", "Customer");
    }
  }
}
