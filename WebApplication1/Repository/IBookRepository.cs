using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModelcs book);
        Task<List<BookModelcs>> GetAllBooks();
        Task<BookModelcs> GetBookById(int id);
        Task<List<BookModelcs>> GetTopBooksAsync(int count);
        List<BookModelcs> SearchBook(string title, string authorName);
        string GetAppName();
    }
}