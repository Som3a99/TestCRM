using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Models.Enums
{
    public enum PriorityLevel
    {
        [Display(Name = "منخفض")]
        Low = 1,
        [Display(Name = "متوسط")]
        Medium = 2,
        [Display(Name = "عالي")]
        High = 3,
        [Display(Name = "عاجل")]
        Urgent = 4
    }
}
