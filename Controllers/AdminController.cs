using FPTBook.Data;
using FPTBook.Models;
using FPTBook.Utils;
using FPTBook.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;



namespace FPTBook.Controllers
{
    [Authorize(Roles = Role.ADMIN)]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<ApplicationUser> _passwordHash;

        public AdminController(
          UserManager<ApplicationUser> userManager, ApplicationDbContext context, IPasswordHasher<ApplicationUser> passwordHash)
        {
            _userManager = userManager;
            _context = context;
            _passwordHash = passwordHash;
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

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStoreOwnerPassword(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Customers");
        }
        [HttpGet]
        public async Task<IActionResult> ChangeCustomerPassword(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Customers");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCustomerPassword(string id, string password)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = _passwordHash.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Customers");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStoreOwnerPassword(string id, string password)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {

                if (!string.IsNullOrEmpty(password))
                    user.PasswordHash = _passwordHash.HashPassword(user, password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                if (!string.IsNullOrEmpty(password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Customers");
                    else
                        Errors(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(user);

        }

        /*public IActionResult ApproveGenre()
        {

        }

        public IActionResult RejectGenre()
        {

        }

        [HttpPost]
        public IActionResult AddApprovedGenre()
        {
            IEnumerable<Genre> genres = _context.Genres.Where(t => t.)
                    .Include(t => t.ApplicationUser)
                    .ToList();
            return View(genres);
        }*/
    }
}
