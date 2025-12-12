using Microsoft.AspNetCore.Mvc;

namespace Servicio_Create.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
