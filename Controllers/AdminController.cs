using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        public AdminController(
          UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Customers()
        {
            var usersWithPermission = _userManager.GetUsersInRoleAsync(Role.CUSTOMER).Result;
            return View(usersWithPermission);
        }

        [HttpGet]
        public IActionResult StoreOwners()
        {
            var usersWithPermission = _userManager.GetUsersInRoleAsync(Role.OWNER).Result;
            return View(usersWithPermission);
        }

    }
}
