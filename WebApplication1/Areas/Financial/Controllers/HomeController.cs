using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Financial.Controllers
{
    [Area("financial")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
