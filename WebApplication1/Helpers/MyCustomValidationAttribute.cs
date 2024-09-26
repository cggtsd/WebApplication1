using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Helpers
{
    public class MyCustomValidationAttribute(string text) : ValidationAttribute
    {
        public string Text { get; set; } = text;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                string bookName=value.ToString();
                if (bookName.Contains("mvc"))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("BookName does not contain the desired value");
            //return new ValidationResult(ErrorMessage??"BookName does not contain the desired value");
        }
    }
}
