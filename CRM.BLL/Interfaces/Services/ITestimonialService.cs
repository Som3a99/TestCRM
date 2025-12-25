using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;

namespace CRM.BLL.Interfaces.Services
{
    public interface ITestimonialService
    {
        Task<TestimonialResponseDto> CreateAsync(CreateTestimonialDto dto);
        Task<bool> UpdateAsync(int id, UpdateTestimonialDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TestimonialDto>> GetPublishedAsync();
        Task<bool> PublishAsync(int id);
        Task<bool> UnpublishAsync(int id);
    }
}
