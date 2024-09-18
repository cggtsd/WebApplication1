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
            return DataSource().Where(x => x.Id == id).FirstOrDefault()??new BookModelcs();
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
                    Author = "Fatima",
                    Description="This is description for MVC book",
                    Category="Programming",
                    Language="English",
                    TotalPages=134
                },
                new BookModelcs()
                {
                    Id = 2,
                    Title = "Dot Net Core",
                    Author = "Fatima",
                    Description="This is description for Dot Net Core book",
                    Category="Framework",
                    Language="English",
                    TotalPages=560
                },
                new BookModelcs()
                {
                    Id = 3,
                    Title = "C#",
                    Author = "D.Fatima",
                    Description="This is description for C# book",
                    Category="Developer",
                    Language="English",
                    TotalPages=897
                },
                new BookModelcs()
                {
                    Id = 4,
                    Title = "Java",
                    Author = "Fariha",
                    Description="This is description for Java book",
                    Category="Concept",
                    Language="English",
                    TotalPages=657
                },
                new BookModelcs()
                {
                    Id = 5,
                    Title = "Php",
                    Author = "Lubna",
                    Description="This is description for Php book",
                    Category="Programming",
                    Language="English",
                    TotalPages=134
                },
                 new BookModelcs()
                {
                    Id = 6,
                    Title = "Azure Devops",
                    Author = "Ravi",
                    Description="This is description for Azure Devops book",
                    Category="Devops",
                    Language="English",
                    TotalPages=800
                }

            };
        }
    }
}
