using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress, Display(Name ="Registred Email Address")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
