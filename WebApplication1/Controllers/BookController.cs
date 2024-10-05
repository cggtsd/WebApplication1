using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Dynamic;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class BookController(BookRepository bookRepository, LanguageRepository languageRepository,IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly BookRepository _bookRepository = bookRepository;
        private readonly LanguageRepository _languageRepository = languageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment=webHostEnvironment;

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> GetAllBooks()
        {
            //return "All books";
            //return _bookRepository.GetAllBooks();
            var books = await _bookRepository.GetAllBooks();
            return View(books);
        }
        [Route("book-details/{id}", Name = "bookDetailRoute")]
        public async Task<IActionResult> GetBook(int id)
        {
            //return $"book with id = {id}";
            //dynamic data = new ExpandoObject();
            //data.book= _bookRepository.GetBookById(id);
            //data.Name = "Fathima";
            var book = await _bookRepository.GetBookById(id);
            return View(book);
        }

        public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModelcs()
            {
                //  Language = "Telugu"
                // Language = "3"
            };
            //ViewBag.Language = new List<string>() { "Urdu", "Telugu", "English" };
            //ViewBag.Language = new SelectList(new List<string>() { "Urdu", "Telugu", "English" });
            //ViewBag.Language=new SelectList(GetLanguages(),"Id","Text");
            //ViewBag.Language = GetLanguages().Select(x => new SelectListItem()
            // {
            //     Text = x.Text,
            //     Value = x.Id.ToString()

            // }).ToList();
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new(){Text="Urdu",Value="1"},
            //    new(){Text="Telugu",Value="2"},
            //    new(){Text="English",Value="3"},
            //    new(){Text="Tamil",Value="4"},
            //    new(){Text="French",Value="5"},
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
            //    new(){Text="Hindi",Value="5",Group=group3},
            //    new(){Text="French",Value="6",Group=group3},
            //};
            ViewBag.Language= new SelectList(await _languageRepository.GetLanguages(),"Id","Name");
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
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl=await UploadImage(folder, bookModel.CoverPhoto);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";
                    bookModel.Gallery = [];
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)

                        };
                        bookModel.Gallery.Add(gallery);

                    }

                    }
                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }
                //_bookRepository.AddNewBook(bookModel);
                int id = await _bookRepository.AddNewBook(bookModel);

                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookid = id });
                    //return RedirectToAction(nameof(AddNewBook));
                }
            }

            //ViewBag.Language = new List<string>() { "Urdu", "Telugu", "English" };
            //ViewBag.Language = new SelectList(new List<string>() { "Urdu", "Telugu", "English" });
            //ViewBag.Language = new SelectList(GetLanguages(), "Id", "Text");
            //ViewBag.Language = GetLanguages().Select(x => new SelectListItem()
            //{
            //    Text = x.Text,
            //    Value = x.Id.ToString()

            //}).ToList();
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new(){Text="Urdu",Value="1"},
            //    new(){Text="Telugu",Value="2"},
            //    new(){Text="English",Value="3"},
            //    new(){Text="Tamil",Value="4"},
            //    new(){Text="French",Value="5"}
            //};
            //var group1 = new SelectListGroup() { Name = "Group1" };
            //var group2 = new SelectListGroup() { Name = "Group2" };
            //var group3 = new SelectListGroup() { Name = "Group3" };
            //ViewBag.Language = new List<SelectListItem>()
            //{
            //    new(){Text="Urdu",Value="1",Group=group1},
            //    new(){Text="Telugu",Value="2",Group=group1},
            //    new(){Text="English",Value="3",Group=group2},
            //    new(){Text="Tamil",Value="4",Group=group2},
            //    new(){Text="Hindi",Value="5",Group=group3},
            //    new(){Text="French",Value="6",Group=group3},
            //};
            ViewBag.Language= new SelectList(await _languageRepository.GetLanguages(),"Id","Name");
            ModelState.AddModelError("", "This is my custom error message");
            ModelState.AddModelError("", "This is my second custom error message");
            return View();
        }

        private  async Task<string> UploadImage(string folderPath, IFormFile file)
        {
            
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;
          
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            return "/"+folderPath;
        }

        public List<BookModelcs> SearchBooks(string bookName, string authorName)
        {
            //return $"Book with name = {bookName} & Author = {authorName}";
            return _bookRepository.SearchBook(bookName, authorName);
        }

        //    private List<LanguageModel> GetLanguages()
        //    {
        //        return
        //        [
        //            new() { Id=1,Text="English"},
        //            new() { Id=2,Text="Urdu"},
        //            new() { Id=3,Text="Telugu"},

        //        ];
        //    }
        //}
    }
}
