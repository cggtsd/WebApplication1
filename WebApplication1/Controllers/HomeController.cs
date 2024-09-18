using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public BookModelcs Book { get; set; }
        [ViewData]
        public string Title { get; set; }

        public IActionResult Index()
        {
            //ViewBag.Name = "CGG";
            //ViewBag.Name = "Fathima";
            //ViewBag.Data = new { Id = 1, Name = "Fathima" };
            //dynamic data = new ExpandoObject();
            //data.Id = 1;
            //data.Name = "Fathima";
            //ViewBag.Data = data;

            //ViewBag.Type = new BookModelcs() { Id = 12, Author = "author" };
            //ViewData["property1"] = "Fathima";
            //ViewData["book"] = new BookModelcs() { Id = 1, Author = "Fathima" };
            //ViewData["Title"] = "Home Page from controller";
            //CustomProperty = "Custom Value";
            Book = new BookModelcs() { Id = 14, Author = "Me" };
            Title = "Home Page";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            ViewData["Title"] = "AboutUs Page from Controller ";
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
