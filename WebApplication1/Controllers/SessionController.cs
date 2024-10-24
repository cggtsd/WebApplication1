using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SessionController : Controller
    {
        [Route("demo1")]
        public IActionResult Demo_1()
        {
           var categoriesString= HttpContext.Session.GetString("CategoriesList");
           List<Categories> categories = new List<Categories>();
           categories=JsonConvert.DeserializeObject<List<Categories>>(categoriesString);

            var userString = HttpContext.Session.GetString("UserData");
            var user=JsonConvert.DeserializeObject<Users>(userString);
            return View();
        }
    }
}
