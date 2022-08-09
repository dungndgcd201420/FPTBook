using Microsoft.AspNetCore.Mvc;

namespace FPTBook.Controllers
{
    public class StoreOwnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
