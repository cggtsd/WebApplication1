using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Dynamic;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class BookController(BookRepository bookRepository) : Controller
    {
        private readonly BookRepository _bookRepository = bookRepository;

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> GetAllBooks()
        {
            //return "All books";
            //return _bookRepository.GetAllBooks();
            var books =  await _bookRepository.GetAllBooks();
            return View(books);
        }
        [Route("book-details/{id}",Name ="bookDetailRoute")]
        public async Task<IActionResult> GetBook(int id)
        {
            //return $"book with id = {id}";
            //dynamic data = new ExpandoObject();
            //data.book= _bookRepository.GetBookById(id);
            //data.Name = "Fathima";
            var book = await _bookRepository.GetBookById(id);
            return View(book);
        }

        public IActionResult AddNewBook(bool isSuccess = false,int bookId=0)
        {
            var model = new BookModelcs()
            {
                //  Language = "Telugu"
                Language = "3"
            };
            //ViewBag.Language = new List<string>() { "Urdu", "Telugu", "English" };
            //ViewBag.Language = new SelectList(new List<string>() { "Urdu", "Telugu", "English" });
            //ViewBag.Language=new SelectList(GetLanguages(),"Id","Text");
            ViewBag.Language = GetLanguages().Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Id.ToString()

            }).ToList();
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new(){Text="Urdu",Value="1",Selected=true},
            //    new(){Text="Telugu",Value="2"},
            //    new(){Text="English",Value="3",Disabled=true},
            //    new(){Text="Tamil",Value="4",Disabled=true},
            //};
            //var group1 = new SelectListGroup() { Name = "Group1" };
            //var group2 = new SelectListGroup() { Name = "Group2" ,Disabled=true};
            //var group3 = new SelectListGroup() { Name = "Group3" };
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new(){Text="Urdu",Value="1",Group=group1},
            //    new(){Text="Telugu",Value="2",Group=group1},
            //    new(){Text="English",Value="3",Group=group2},
            //    new(){Text="Tamil",Value="4",Group=group2},
            //    new(){Text="Dutch",Value="5",Group=group3},
            //    new(){Text="French",Value="6",Group=group3},
            //};
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
            //return View(model);
            }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModelcs bookModel)
        {
            if (ModelState.IsValid)
            {
                //_bookRepository.AddNewBook(bookModel);
                int id = await _bookRepository.AddNewBook(bookModel);

            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook), new { isSuccess = true ,bookid=id});
                //return RedirectToAction(nameof(AddNewBook));
            }
            }

            //ViewBag.Language = new List<string>() { "Urdu", "Telugu", "English" };
            //ViewBag.Language = new SelectList(new List<string>() { "Urdu", "Telugu", "English" });
            //ViewBag.Language = new SelectList(GetLanguages(), "Id", "Text");
            ViewBag.Language = GetLanguages().Select(x => new SelectListItem()
            {
                Text = x.Text,
                Value = x.Id.ToString()

            }).ToList();
            ModelState.AddModelError("", "This is my custom error message");
            ModelState.AddModelError("", "This is my second custom error message");
            return View();
        }

        public List<BookModelcs> SearchBooks(string bookName,string authorName)
        {
            //return $"Book with name = {bookName} & Author = {authorName}";
            return _bookRepository.SearchBook(bookName, authorName);
        }

        private List<LanguageModel> GetLanguages()
        {
            return
            [
                new() { Id=1,Text="English"},
                new() { Id=2,Text="Urdu"},
                new() { Id=3,Text="Telugu"},

            ];
        }
    }
}
