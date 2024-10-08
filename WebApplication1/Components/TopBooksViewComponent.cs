using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repository;

namespace WebApplication1.Components
{
    public class TopBooksViewComponent(IBookRepository bookRepository):ViewComponent
    {
        private readonly IBookRepository _bookRepository = bookRepository;
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooksAsync(count);
            return View(books);
            //return View();
        }
    }
}
