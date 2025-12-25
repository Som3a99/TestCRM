using System.ComponentModel.DataAnnotations;

namespace CRM.DAL.Models.Enums
{
    public enum ResourceType
    {
        [Display(Name = "فيديو")]
        Video = 1,
        [Display(Name = "صورة")]
        Image = 2,
        [Display(Name = "صوت")]
        Audio = 3
    }

}
