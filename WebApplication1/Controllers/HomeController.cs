using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Dynamic;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Service;

namespace WebApplication1.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IConfiguration _configuration;
        private readonly NewBookAlertConfig _newBookAlertConfiguration;
        private readonly NewBookAlertConfig _thirdPartyBookConfiguration;
        private readonly IMessageRepository _messageRepository
;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger,
         IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfiguration,
       //IConfiguration configuration,
       IMessageRepository messageRepository
      , IUserService userService
      ,IEmailService emailService
      )
        {
            _logger = logger;
            //_configuration = configuration;
            //_newBookAlertConfiguration = newBookAlertConfiguration.Value;
            _newBookAlertConfiguration = newBookAlertConfiguration.Get("InternalBook");
            _thirdPartyBookConfiguration = newBookAlertConfiguration.Get("ThirdPartyBook");
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
        }
        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public BookModelcs Book { get; set; }
        [ViewData]
        public string Title { get; set; }
        //[Route("")]
        [Route("~/")]

        public async Task<IActionResult> Index()
        {
            //var userId = _userService.GetUserId();
            //var isLoggedIn = _userService.IsAuthenticated();

            //UserEmailOptions options = new()
            //{
            //    ToEmails = ["user@test.com"],
            //    Placeholders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{Username}}","Fathima")
            //    }
            //};
            // await _emailService.SendTestEmail(options);
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
            //var result = _configuration["DisplayNewBookAlert"];
            //var _ = _configuration.GetValue<bool>("DisplayNewBookAlert");
            //var result = _configuration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");
            //var bookName = _configuration.GetValue<string>("NewBookAlert:BookName");

            //var newBook = _configuration.GetSection("NewBookAlert");
            //var result = newBook.GetValue<bool>("DisplayNewBookAlert");
            //var bookName = newBook.GetValue<string>("BookName");
            //var result = _configuration["AppName"];
            //var key1 = _configuration["infoObj:key1"];
            //var key2 = _configuration["infoObj:key2"];
            //var key3 = _configuration["infoObj:key3:key3obj1"];
            //var newBookAlert = new NewBookAlertConfig();
            //_configuration.Bind("NewBookAlert", newBookAlert);
            //bool isDisplay = newBookAlert.DisplayNewBookAlert;
            //bool isDisplay = _newBookAlertConfiguration.DisplayNewBookAlert;
            //bool isDisplay = _newBookAlertConfiguration.DisplayNewBookAlert;
            //bool isDisplay1 = _thirdPartyBookConfiguration.DisplayNewBookAlert;
            //var value = _messageRepository.GetName();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        //[Route("about-us/{id}/test/{name}")]
        //[Route("about-us")]
        //[HttpGet]
        //[HttpGet("about-us")]
        //[HttpGet("about-us",Name ="about-us",Order =1)]
        [Route("~/about-us/{name:alpha:minlength(5)}")]
        public IActionResult AboutUs(string name)
        {
            ViewData["Title"] = "AboutUs Page from Controller ";
            return View();
        }
        //[Route("~/contact-us")]
        [Route("~/contact-us", Name = "contact-us")]
        [Authorize(Roles="Admin")]
        public IActionResult ContactUs()
        {
            return View();
        }
        [Route("~/test/a{a}")]
        public string Test(string a)
        {
            return a;
        }
        [Route("~/test/b{a}")]
        public string Test1(string a)
        {
            return a;
        }
        [Route("~/test/c{a}")]
        public string Test2(string a)
        {
            return a;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
