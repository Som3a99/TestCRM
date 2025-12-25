using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Models.Enums
{
    public enum ContactMethod
    {
        [Display(Name = "مكالمة هاتفية")]
        Call = 1,
        [Display(Name = "واتس اب")]
        WhatsApp = 2,
        [Display(Name = "البريد الإليكتروني")]
        Email = 3
    }
}
