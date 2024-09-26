using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Enums
{
    public enum LanguageEnum
    {
        [Display(Name ="English - Language")]
        English=10,
        [Display(Name = "Urdu - Language")]
        Urdu =11,
        [Display(Name = "Telugu - Language")]
        Telugu =12,
        [Display(Name = "Hindi - Language")]
        Hindi =13,
        [Display(Name = "French - Language")]
        French =14,
        [Display(Name = "Tamil - Language")]
        Tamil =15
    }
}
