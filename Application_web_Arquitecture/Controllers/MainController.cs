using Microsoft.AspNetCore.Mvc;

namespace Application_web_Arquitecture.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Configuration()
        {
            return View();
        }
    }
}
