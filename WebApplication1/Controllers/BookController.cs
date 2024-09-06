using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public BookModelcs GetBook(int id)
        {
            //return $"book with id = {id}";
            return _bookRepository.GetBookById(id);
        }

        public List<BookModelcs> SearchBooks(string bookName,string authorName)
        {
            //return $"Book with name = {bookName} & Author = {authorName}";
            return _bookRepository.SearchBook(bookName, authorName);
        }
    }
}
