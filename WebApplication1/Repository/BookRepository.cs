using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class BookRepository
    {

        public List<BookModelcs> GetAllBooks()
        {
            return DataSource();
        }
        public BookModelcs GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookModelcs> SearchBook(string title, string authorName)
        {
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();

        }

        private List<BookModelcs> DataSource()
        {

            return new List<BookModelcs>()
            {
                new BookModelcs()
                {
                    Id = 1,
                    Title = "MVC",
                    Author = "Fatima"
                },
                new BookModelcs()
                {
                    Id = 2,
                    Title = "Dot Net Core",
                    Author = "Fatima"
                },
                new BookModelcs()
                {
                    Id = 3,
                    Title = "C#",
                    Author = "D.Fatima"
                },
                new BookModelcs()
                {
                    Id = 4,
                    Title = "Java",
                    Author = "Fariha"
                },
                new BookModelcs()
                {
                    Id = 5,
                    Title = "Php",
                    Author = "Lubna"
                }

            };
        }
    }
}
