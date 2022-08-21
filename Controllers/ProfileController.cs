using FPTBook.Data;
using FPTBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace FPTBook.Controllers
{
  public class ProfileController : Controller
  {
    private ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public ProfileController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }
    public async Task<IActionResult> Index()
    {
      var currentUserId = _userManager.GetUserId(User);
      var orders = _context.Orders.Include(t => t.CartList)
        .Where(t => t.UserId == currentUserId).ToList();
      var currentUser =  await _userManager.FindByIdAsync(currentUserId);
      var profileInDb = _context.Profiles;
      var profiles = _context.Profiles
        .Include(t => t.User)
        .Where(t => t.UserId == currentUserId)
        .ToList();
      var profile = _context.Profiles.SingleOrDefault(p => p.UserId == currentUserId);

      //Remove Previous Profile before creating new one if there is already a Profile
      if(profiles.Any())
      {
        profileInDb.Remove(profile);
        await _context.SaveChangesAsync();
      }
      
      var newProfile = new Profile()
        {
          UserId = currentUserId,
          User = currentUser,
          Orders = orders
        };
        profileInDb.Add(newProfile);
        await _context.SaveChangesAsync();

      return View(profile);
     }
}
}


