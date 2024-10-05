using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Components
{
    public class TopBooksViewComponent(BookRepository bookRepository) : ViewComponent
    {
        //private readonly BookRepository _bookRepository = bookRepository;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var books = await _bookRepository.GetTopBooksAsync();
            //return View(books);
            return View();
        }
    }
}
