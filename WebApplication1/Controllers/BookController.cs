using Microsoft.AspNetCore.Mvc;
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
            //var model = new BookModelcs()
            //{
            //    Language = "Urdu"
            //};
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModelcs bookModel)
        {
            //if (ModelState.IsValid)
            //{
            //_bookRepository.AddNewBook(bookModel);
            int id = await _bookRepository.AddNewBook(bookModel);

            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook), new { isSuccess = true ,bookid=id});
                //return RedirectToAction(nameof(AddNewBook));
            }
       // }

            //ModelState.AddModelError("", "This is my custom error message");
            //ModelState.AddModelError("", "This is my second custom error message");
            return View();
        }

        public List<BookModelcs> SearchBooks(string bookName,string authorName)
        {
            //return $"Book with name = {bookName} & Author = {authorName}";
            return _bookRepository.SearchBook(bookName, authorName);
        }
    }
}
