using CRM.BLL.Interfaces.Repositories;
using CRM.DAL;
using CRM.DAL.Models;
using CRM.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CRM.BLL.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> GetPublishedAsync()
        {
            return await _context.Projects
                .Include(p => p.ProjectTechnologies)
                    .ThenInclude(pt => pt.Technology)
                .Where(p => p.IsPublished && !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetBySystemTypeAsync(SystemType systemType)
        {
            return await _context.Projects
                .Include(p => p.ProjectTechnologies)
                    .ThenInclude(pt => pt.Technology)
                .Where(p => p.SystemType == systemType && p.IsPublished && !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Project?> GetWithDetailsAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.ProjectTechnologies)
                    .ThenInclude(pt => pt.Technology)
                .Include(p => p.ProgrammersOfProject)
                    .ThenInclude(pp => pp.Programmer)
                .Include(p => p.ProjectResources)
                .Include(p => p.Testimonials)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Project>> GetFeaturedAsync(int count)
        {
            return await _context.Projects
                .Include(p => p.ProjectTechnologies)
                    .ThenInclude(pt => pt.Technology)
                .Where(p => p.IsPublished && !p.IsDeleted)
                .OrderByDescending(p => p.ViewCount)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task IncrementViewCountAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null && !project.IsDeleted)
            {
                project.ViewCount++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<SystemType, int>> GetCountByTypeAsync()
        {
            return await _context.Projects
                .Where(p => !p.IsDeleted)
                .GroupBy(p => p.SystemType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Type, x => x.Count);
        }
    }
}
