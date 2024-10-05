using System.ComponentModel.DataAnnotations;
using WebApplication1.Enums;
using WebApplication1.Helpers;

namespace WebApplication1.Models
{
    public class BookModelcs
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Choose  an Email")]
        [EmailAddress]
        public string MyField { get; set; }
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the book tilte")]
        //[MyCustomValidation("abc")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Please enter the author name")]
        public string Author { get; set; }
        [StringLength(500)]
        [Required(ErrorMessage = "Please enter some description")]
        public string Description { get; set; }
        public string? Category { get; set; }
        [Required(ErrorMessage = "Please choose language of your book")]
        [Display(Name = "Choose language of your book")]
        public int LanguageId { get; set; }
        public string? Language { get; set; }
        //public int Language { get; set; }
        //[Required(ErrorMessage = "Please choose the languages of your book")]
        //public List<string> MultiLanguage { get; set; }
        //[Required(ErrorMessage = "Please choose the languages of your book")]
        //public LanguageEnum? LanguageEnum { get; set; }
        [Required(ErrorMessage = "Please enter number of pages in the book")]
        [Display(Name = "Total Pages of Book")]
        public int? TotalPages { get; set; }
        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string? CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery image of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryModel>? Gallery { get; set; }
        [Display(Name = "Choose your book in pdf format")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string? BookPdfUrl { get; set; }
    }
}
