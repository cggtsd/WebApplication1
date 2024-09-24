using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BookModelcs
    {
        //[DataType(DataType.DateTime)]
        //[Display(Name ="Choose Date and Time")]
        //public string MyField { get; set; }
        public int Id { get; set; }
        //[StringLength(100,MinimumLength =5)]
        //[Required(ErrorMessage ="Please enter the title")]
        public string Title { get; set; }
        //[Required(ErrorMessage ="Please enter the author name")]
        public string Author { get; set; }
        //[StringLength(500)]
        public string Description { get; set; }
        public string Category { get; set; }
        //[Required(ErrorMessage ="Please choose language of your book")]
        public string Language { get; set; }
        //[Required(ErrorMessage ="Please enter the total pages")]
        //[Display(Name ="Total Pages of Book")]
        public int TotalPages { get; set; }
    }
}
