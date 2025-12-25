using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Models.Enums
{
    public enum InquiryStatus
    {
        [Display(Name = "جديد")]
        New = 1,
        [Display(Name = "قيد المعالجة")]
        InProgress = 2,
        [Display(Name = "تم الرد")]
        Responded = 3,
        [Display(Name = "تم التحويل لعميل")]
        Converted = 4,
        [Display(Name = "مرفوض")]
        Rejected = 5
    }
}
