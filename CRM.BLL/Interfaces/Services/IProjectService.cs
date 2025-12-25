using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.DAL.Models.Enums;

namespace CRM.BLL.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto);
        Task<bool> UpdateAsync(int id, UpdateProjectDto dto);
        Task<bool> DeleteAsync(int id);
        Task<ProjectDetailsDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProjectCardDto>> GetPublishedAsync();
        Task<IEnumerable<ProjectCardDto>> GetBySystemTypeAsync(SystemType type);
        Task<bool> PublishAsync(int id);
        Task<bool> UnpublishAsync(int id);
        Task IncrementViewAsync(int id);
    }
}
