using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController()
        {
            _bookRepository = new BookRepository();
            
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult GetAllBooks()
        {
            //return "All books";
            //return _bookRepository.GetAllBooks();
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }
        [Route("book-details/{id}",Name ="bookDetailRoute")]
        public IActionResult GetBook(int id)
        {
            //return $"book with id = {id}";
            dynamic data = new ExpandoObject();
            data.book= _bookRepository.GetBookById(id);
            data.Name = "Fathima";
            //var book= 
            return View(data);
        }

        public IActionResult AddNewBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBook(BookModelcs bookModel)
        {
            return View();
        }

        public List<BookModelcs> SearchBooks(string bookName,string authorName)
        {
            //return $"Book with name = {bookName} & Author = {authorName}";
            return _bookRepository.SearchBook(bookName, authorName);
        }
    }
}
