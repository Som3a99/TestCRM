using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Models.Enums
{
    public enum ResponsibilityType
    {
        [Display(Name = "تصميم موقع ويب")]
        WebApplication = 1,
        [Display(Name = "تصميم تطبيق سطح مكتب")]
        DesktopApplication = 2,
        [Display(Name = "تصميم تطبيق هاتف")]
        MobileApplication = 3,
        [Display(Name = "ذكاء اصطناعي")]
        AI = 4,
        [Display(Name = "تصميم واجهة المستخدم وتجربة المستخدم")]
        UiUxDesign = 5
    }
}
