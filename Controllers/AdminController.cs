using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
