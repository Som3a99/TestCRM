using CRM.BLL.Interfaces.Repositories;
using CRM.DAL;
using CRM.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.BLL.Repositories
{
    public class TechnologyRepository : GenericRepository<Technology>, ITechnologyRepository
    {
        public TechnologyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Technology>> GetActiveAsync()
        {
            return await _context.Technologies
                .Where(t => t.IsActive && !t.IsDeleted)
                .OrderBy(t => t.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Technology>> GetByCategoryAsync(string category)
        {
            return await _context.Technologies
                .Where(t => t.Category == category && t.IsActive && !t.IsDeleted)
                .OrderBy(t => t.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Technology>> GetByProjectAsync(int projectId)
        {
            return await _context.Technologies
                .Where(t => t.TechnologyOfProjects.Any(tp => tp.ProjectId == projectId) && !t.IsDeleted)
                .OrderBy(t => t.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Technologies
                .AnyAsync(t => t.TechnologyName == name && !t.IsDeleted);
        }
    }
}
