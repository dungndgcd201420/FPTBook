using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
