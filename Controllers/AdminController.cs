using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using FPTBook.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Threading.Tasks;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return View();
            }

            return View();
        }
    }
}
