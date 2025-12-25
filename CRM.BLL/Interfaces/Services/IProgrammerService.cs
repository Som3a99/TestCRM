using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;

namespace CRM.BLL.Interfaces.Services
{
    public interface IProgrammerService
    {
        Task<ProgrammerResponseDto> CreateAsync(CreateProgrammerDto dto);
        Task<bool> UpdateAsync(int id, UpdateProgrammerDto dto);
        Task<IEnumerable<ProgrammerCardDto>> GetActiveAsync();
        Task<ProgrammerDetailsDto?> GetByIdAsync(int id);
        Task<bool> DeactivateAsync(int id);
    }
}
