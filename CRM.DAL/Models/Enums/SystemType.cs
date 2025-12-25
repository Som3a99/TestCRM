using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Models.Enums
{
    public enum SystemType
    {
        [Display(Name = "متجر اليكتروني")]
        ECommerce = 1,
        [Display(Name = "ERP System")]
        ERPSystem = 2,
        [Display(Name = "CRM System")]
        CRMSystem = 3,
        [Display(Name = "صفحات هبوط (تعريفية)")]
        LandingPage = 4
    }
}
