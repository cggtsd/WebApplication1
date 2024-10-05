using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Dynamic;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IConfiguration _configuration;
        private readonly NewBookAlertConfig _newBookAlertConfiguration;
        private readonly IMessageRepository _messageRepository
;        public HomeController(ILogger<HomeController> logger,IOptions<NewBookAlertConfig> newBookAlertConfiguration,IMessageRepository messageRepository)
        {
            _logger = logger;
            //_configuration = configuration;
            _newBookAlertConfiguration = newBookAlertConfiguration.Value;
            _messageRepository = messageRepository;
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
            //var _ = _configuration.GetValue<bool>("DisplayNewBookAlert");
            //var result = _configuration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");
            //var bookName = _configuration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");

            //var newBook= _configuration.GetSection("NewBookAlert");
            //var result = newBook.GetValue<bool>("DisplayNewBookAlert");
            //var bookName = newBook.GetValue<bool>("BookName");
            //var result = _configuration["AppName"];
            //var key1 = _configuration["infoObj:key1"];
            //var key2 = _configuration["infoObj:key2"];
            //var key3 = _configuration["infoObj:key3:key3obj1"];
            //var newBookAlert = new NewBookAlertConfig();
            //_configuration.Bind("NewBookAlert", newBookAlert);
            //bool isDisplay = newBookAlert.DisplayNewBookAlert;
            bool isDisplay = _newBookAlertConfiguration.DisplayNewBookAlert;
            var value = _messageRepository.GetName();

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
