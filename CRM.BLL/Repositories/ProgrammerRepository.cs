using CRM.BLL.Interfaces.Repositories;
using CRM.DAL;
using CRM.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM.BLL.Repositories
{
    public class ProgrammerRepository : GenericRepository<Programmer>, IProgrammerRepository
    {
        public ProgrammerRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Programmer>> GetActiveAsync()
        {
            return await _context.Programmers
                .Where(p => p.IsActive && !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Programmer?> GetWithProjectsAsync(int id)
        {
            return await _context.Programmers
                .Include(p => p.ProjectsOfProgrammer)
                    .ThenInclude(pp => pp.Project)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<IEnumerable<Programmer>> GetByProjectAsync(int projectId)
        {
            return await _context.Programmers
                .Where(p => p.ProjectsOfProgrammer.Any(pp => pp.ProjectId == projectId) && !p.IsDeleted)
                .OrderBy(p => p.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetProjectCountPerProgrammerAsync()
        {
            return await _context.Programmers
                .Where(p => !p.IsDeleted)
                .Select(p => new
                {
                    ProgrammerId = p.Id,
                    ProjectCount = p.ProjectsOfProgrammer.Count(pp => !pp.IsDeleted)
                })
                .ToDictionaryAsync(x => x.ProgrammerId, x => x.ProjectCount);
        }
    }
}
