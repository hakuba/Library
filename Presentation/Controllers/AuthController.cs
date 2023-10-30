using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
