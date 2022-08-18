using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      var orderInDb = _context.Orders;
  
      IEnumerable<Cart> booksInCart = _context.Carts
            .Include(t=> t.Book)
            .Where(t => t.UserId == currentUserId)
            .ToList();

      return View(booksInCart);
    }

    public IActionResult QuantityUp(int id)
    {
      var bookInCart = _context.Carts.SingleOrDefault(t => t.Id == id);
        if (bookInCart == null)
      {
        return NotFound();
      }
      bookInCart.Quantity += 1;
      _context.SaveChanges();
      return RedirectToAction("Index");
    }
    public IActionResult QuantityDown(int id)
    {
      var bookInCart = _context.Carts.SingleOrDefault(t => t.Id == id);
      if (bookInCart == null)
      {
        return NotFound();
      }
      if (bookInCart.Quantity > 1)
      {
        bookInCart.Quantity -= 1;
      }
      _context.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var cartBook = _context.Carts;
      var bookInCart = _context.Carts.SingleOrDefault(t => t.Id == id);
      if (bookInCart == null)
      {
        return NotFound();
      }
      cartBook.Remove(bookInCart);
      await _context.SaveChangesAsync();
      return RedirectToAction("Index");

    }
    public async Task<IActionResult> CheckOut()
    { 
     

      var currentUserId = _userManager.GetUserId(User);
      var orderInDb = _context.Orders;

      var ordersNotPaid = _context.Orders
      .Include(t => t.CartList)
      .Where(t => t.UserId == currentUserId && t.OrderStatus == Enums.OrderStatus.notPaid)
      .ToList();

      var cartBooks = _context.Carts
        .Include(t => t.Book)
        .Where(t => t.UserId == currentUserId)
        .ToList();

      float total = 0;

     
      //Remove all the unpaid
      foreach(var order in ordersNotPaid)
      {
        orderInDb.Remove(order);
        await _context.SaveChangesAsync();
      }
      //Get total
      foreach (var book in cartBooks)
      {
        total += (book.Price * book.Quantity);
      }
      //Add New Order
        var newOrder = new Order()
          {
            CartList = cartBooks,
            Total = total,
            OrderStatus = Enums.OrderStatus.notPaid,
            UserId = currentUserId
          };
          orderInDb.Add(newOrder);
          await _context.SaveChangesAsync();

      return RedirectToAction("Index", "Order");

    }

  }
  }

