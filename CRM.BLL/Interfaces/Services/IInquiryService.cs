using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Interfaces.Services
{
    public interface IInquiryService
    {
        Task<InquiryResponseDto> CreateAsync(CreateInquiryDto dto);
        Task<InquiryResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<InquiryListDto>> GetAllAsync();
        Task<IEnumerable<InquiryListDto>> GetByStatusAsync(InquiryStatus status);
        Task<bool> AssignToProgrammerAsync(int inquiryId, int programmerId);
        Task<bool> UpdateStatusAsync(int inquiryId, InquiryStatus status, string? notes);
        Task<bool> ConvertToCustomerAsync(int inquiryId, int customerId);
        Task<InquiryStatisticsDto> GetStatisticsAsync();
    }
}
