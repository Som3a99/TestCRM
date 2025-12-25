using AutoMapper;
using CRM.BLL.DTOs.Requests;
using CRM.BLL.DTOs.Responese;
using CRM.BLL.Exceptions;
using CRM.BLL.Interfaces;
using CRM.BLL.Interfaces.Services;
using CRM.DAL.Models;
using CRM.DAL.Models.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace CRM.BLL.Services
{
    public class ProgrammerService : IProgrammerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public ProgrammerService(IUnitOfWork unitOfWork, IMemoryCache cache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<ProgrammerResponseDto> CreateAsync(CreateProgrammerDto dto)
        {
            var programmer = _mapper.Map<Programmer>(dto);

            await _unitOfWork.Programmers.AddAsync(programmer);
            await _unitOfWork.SaveChangesAsync();

            _cache.Remove("active_programmers");

            return _mapper.Map<ProgrammerResponseDto>(programmer);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProgrammerDto dto)
        {
            var programmer = await _unitOfWork.Programmers.GetByIdAsync(id);
            if (programmer == null)
                throw new NotFoundException(nameof(Programmer), id);

            _mapper.Map(dto, programmer);

            _unitOfWork.Programmers.Update(programmer);

            _cache.Remove("active_programmers");
            _cache.Remove($"programmer_details_{id}");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ProgrammerCardDto>> GetActiveAsync()
        {
            const string cacheKey = "active_programmers";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProgrammerCardDto>? cached))
            {
                var programmers = await _unitOfWork.Programmers.GetActiveAsync();
                cached = _mapper.Map<IEnumerable<ProgrammerCardDto>>(programmers);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(cacheKey, cached, cacheOptions);
            }

            return cached ?? Enumerable.Empty<ProgrammerCardDto>();
        }

        public async Task<ProgrammerDetailsDto?> GetByIdAsync(int id)
        {
            var cacheKey = $"programmer_details_{id}";

            if (!_cache.TryGetValue(cacheKey, out ProgrammerDetailsDto? cached))
            {
                var programmer = await _unitOfWork.Programmers.GetWithProjectsAsync(id);
                if (programmer == null)
                    return null;

                cached = _mapper.Map<ProgrammerDetailsDto>(programmer);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                _cache.Set(cacheKey, cached, cacheOptions);
            }

            return cached;
        }

        public async Task<bool> DeactivateAsync(int id)
        {
            var programmer = await _unitOfWork.Programmers.GetByIdAsync(id);
            if (programmer == null)
                throw new NotFoundException(nameof(Programmer), id);

            programmer.IsActive = false;
            _unitOfWork.Programmers.Update(programmer);

            _cache.Remove("active_programmers");
            _cache.Remove($"programmer_details_{id}");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }
    }

}
