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
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMemoryCache cache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto)
        {
            var project = _mapper.Map<Project>(dto);

            await _unitOfWork.Projects.AddAsync(project);
            await _unitOfWork.SaveChangesAsync();

            // Add technologies
            foreach (var techId in dto.TechnologyIds)
            {
                var projectTech = new ProjectTechnology
                {
                    ProjectId = project.Id,
                    TechnologyId = techId
                };
                await _unitOfWork.Repository<ProjectTechnology>().AddAsync(projectTech);
            }

            // Add programmers
            foreach (var progId in dto.ProgrammerIds)
            {
                var projectProg = new ProjectProgrammer
                {
                    ProjectId = project.Id,
                    ProgrammerId = progId,
                    Description = "Project contribution",
                    ResponsibilityType = ResponsibilityType.WebApplication
                };
                await _unitOfWork.Repository<ProjectProgrammer>().AddAsync(projectProg);
            }

            await _unitOfWork.SaveChangesAsync();

            // Clear cache
            _cache.Remove("published_projects");

            return _mapper.Map<ProjectResponseDto>(project);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProjectDto dto)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new NotFoundException(nameof(Project), id);

            _mapper.Map(dto, project);

            _unitOfWork.Projects.Update(project);

            // Clear cache
            _cache.Remove("published_projects");
            _cache.Remove($"project_details_{id}");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new NotFoundException(nameof(Project), id);

            project.IsDeleted = true;
            _unitOfWork.Projects.Update(project);

            _cache.Remove("published_projects");
            _cache.Remove($"project_details_{id}");

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<ProjectDetailsDto?> GetByIdAsync(int id)
        {
            var cacheKey = $"project_details_{id}";

            if (!_cache.TryGetValue(cacheKey, out ProjectDetailsDto? cachedProject))
            {
                var project = await _unitOfWork.Projects.GetWithDetailsAsync(id);
                if (project == null)
                    return null;

                cachedProject = _mapper.Map<ProjectDetailsDto>(project);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(cacheKey, cachedProject, cacheOptions);
            }

            return cachedProject;
        }

        public async Task<IEnumerable<ProjectCardDto>> GetPublishedAsync()
        {
            const string cacheKey = "published_projects";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<ProjectCardDto>? cachedProjects))
            {
                var projects = await _unitOfWork.Projects.GetPublishedAsync();
                cachedProjects = _mapper.Map<IEnumerable<ProjectCardDto>>(projects);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                _cache.Set(cacheKey, cachedProjects, cacheOptions);
            }

            return cachedProjects ?? Enumerable.Empty<ProjectCardDto>();
        }

        public async Task<IEnumerable<ProjectCardDto>> GetBySystemTypeAsync(SystemType type)
        {
            var projects = await _unitOfWork.Projects.GetBySystemTypeAsync(type);
            return _mapper.Map<IEnumerable<ProjectCardDto>>(projects);
        }

        public async Task<bool> PublishAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new NotFoundException(nameof(Project), id);

            project.IsPublished = true;
            _unitOfWork.Projects.Update(project);

            _cache.Remove("published_projects");
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UnpublishAsync(int id)
        {
            var project = await _unitOfWork.Projects.GetByIdAsync(id);
            if (project == null)
                throw new NotFoundException(nameof(Project), id);

            project.IsPublished = false;
            _unitOfWork.Projects.Update(project);

            _cache.Remove("published_projects");
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task IncrementViewAsync(int id)
        {
            await _unitOfWork.Projects.IncrementViewCountAsync(id);
        }
    }
}
