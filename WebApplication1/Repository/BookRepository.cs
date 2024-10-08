using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class BookRepository(BookStoreContext context,IConfiguration configuration) : IBookRepository
    {

        private readonly BookStoreContext _context = context;
        private readonly IConfiguration _configuration = configuration;

        public async Task<int> AddNewBook(BookModelcs book)
        {
            var newBook = new Books()
            {
                Author = book.Author,
                CreatedOn = DateTime.UtcNow,
                Description = book.Description,
                Title = book.Title,
                LanguageId = book.LanguageId,
                TotalPages = (int)(book.TotalPages ?? 0),

                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl,
            };

            newBook.bookGallery = new List<BookGallery>();
            foreach (var file in book.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL,
                });
            }
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<List<BookModelcs>> GetAllBooks()
        {
            //var books = new List<BookModelcs>();
            //var allbooks = await _context.Books.ToListAsync();
            //if (allbooks?.Any() == true)
            //{
            //    foreach (var book in allbooks)
            //    {
            //        books.Add(new BookModelcs()
            //        {
            //            Author = book.Author,
            //            Title = book.Title,
            //            TotalPages = book.TotalPages,
            //            Category = book.Category,
            //            Description = book.Description,
            //            Id = book.Id,
            //            LanguageId = book.LanguageId,
            //           Language=book.Language.Name,
            //           CoverImageUrl = book.CoverImageUrl,


            //        }
            //            );


            //    }
            //}

            //return books;
            //return DataSource();
            return await _context.Books.Select(x => new BookModelcs()
            {
                Author = x.Author,
                Title = x.Title,
                TotalPages = x.TotalPages,
                Category = x.Category,
                Description = x.Description,
                Id = x.Id,
                Language = x.Language.Name,
                CoverImageUrl = x.CoverImageUrl,

            }).ToListAsync();
        }
        public async Task<List<BookModelcs>> GetTopBooksAsync(int count)
        {
            return await _context.Books.Select(x => new BookModelcs()
            {
                Author = x.Author,
                Title = x.Title,
                TotalPages = x.TotalPages,
                Category = x.Category,
                Description = x.Description,
                Id = x.Id,
                Language = x.Language.Name,
                CoverImageUrl = x.CoverImageUrl,

            }).Take(count).ToListAsync();
        }
        public async Task<BookModelcs> GetBookById(int id)
        {
            //var book = await _context.Books.FindAsync(id);
            return await _context.Books.Where(x => x.Id == id).Select(x => new BookModelcs()
            {
                Author = x.Author,
                Title = x.Title,
                TotalPages = x.TotalPages,
                Category = x.Category,
                Description = x.Description,
                Id = x.Id,
                Language = x.Language.Name,
                CoverImageUrl = x.CoverImageUrl,
                BookPdfUrl = x.BookPdfUrl,
                Gallery = x.bookGallery.Select(g => new GalleryModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    URL = g.URL
                }).ToList(),



            }).FirstOrDefaultAsync() ?? new BookModelcs();

            //if (book != null)
            //{
            //    var bookDetails = new BookModelcs()
            //    {
            //        Author = book.Author,
            //        Title = book.Title,
            //        TotalPages = book.TotalPages,
            //        Category = book.Category,
            //        Description = book.Description,
            //        Id = book.Id,
            //        LanguageId = book.LanguageId,
            //        //Language= book.Language.Name
            //    };

            //    return bookDetails;
            //}
            //return null;

            //return DataSource().Where(x => x.Id == id).FirstOrDefault() ?? new BookModelcs();
            //return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }

        public List<BookModelcs> SearchBook(string title, string authorName)
        {
            return null;
            //return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorName)).ToList();

        }

        //private List<BookModelcs> DataSource()
        //{

        //    return new List<BookModelcs>()
        //    {
        //        new BookModelcs()
        //        {
        //            Id = 1,
        //            Title = "MVC",
        //            Author = "Fatima",
        //            Description="This is description for MVC book",
        //            Category="Programming",
        //            Language="English",
        //            TotalPages=134
        //        },
        //        new BookModelcs()
        //        {
        //            Id = 2,
        //            Title = "Dot Net Core",
        //            Author = "Fatima",
        //            Description="This is description for Dot Net Core book",
        //            Category="Framework",
        //            Language="English",
        //            TotalPages=560
        //        },
        //        new BookModelcs()
        //        {
        //            Id = 3,
        //            Title = "C#",
        //            Author = "D.Fatima",
        //            Description="This is description for C# book",
        //            Category="Developer",
        //            Language="English",
        //            TotalPages=897
        //        },
        //        new BookModelcs()
        //        {
        //            Id = 4,
        //            Title = "Java",
        //            Author = "Fariha",
        //            Description="This is description for Java book",
        //            Category="Concept",
        //            Language="English",
        //            TotalPages=657
        //        },
        //        new BookModelcs()
        //        {
        //            Id = 5,
        //            Title = "Php",
        //            Author = "Lubna",
        //            Description="This is description for Php book",
        //            Category="Programming",
        //            Language="English",
        //            TotalPages=134
        //        },
        //         new BookModelcs()
        //        {
        //            Id = 6,
        //            Title = "Azure Devops",
        //            Author = "Ravi",
        //            Description="This is description for Azure Devops book",
        //            Category="Devops",
        //            Language="English",
        //            TotalPages=800
        //        }

        //    };
        //}

        public string GetAppName()
        {
            return "Book Store Application";
            //return _configuration["AppName"];
        }
    }

}
