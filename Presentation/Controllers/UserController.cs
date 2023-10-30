using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
