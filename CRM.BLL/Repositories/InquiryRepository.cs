using CRM.BLL.Interfaces.Repositories;
using CRM.DAL;
using CRM.DAL.Models;
using CRM.DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CRM.BLL.Repositories
{
    public class InquiryRepository : GenericRepository<Inquiry>, IInquiryRepository
    {
        public InquiryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Inquiry>> GetByStatusAsync(InquiryStatus status)
        {
            return await _context.Inquiries
                .Include(i => i.AssignedToProgrammer)
                .Where(i => i.Status == status && !i.IsDeleted)
                .OrderByDescending(i => i.InquiryDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Inquiry>> GetUnassignedAsync()
        {
            return await _context.Inquiries
                .Where(i => i.AssignedToProgrammerId == null && !i.IsDeleted)
                .OrderBy(i => i.Priority)
                .ThenByDescending(i => i.InquiryDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Inquiry>> GetByProgrammerAsync(int programmerId)
        {
            return await _context.Inquiries
                .Include(i => i.AssignedToProgrammer)
                .Where(i => i.AssignedToProgrammerId == programmerId && !i.IsDeleted)
                .OrderByDescending(i => i.InquiryDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Inquiry>> GetHighPriorityAsync()
        {
            return await _context.Inquiries
                .Include(i => i.AssignedToProgrammer)
                .Where(i => (i.Priority == PriorityLevel.High || i.Priority == PriorityLevel.Urgent)
                    && i.Status != InquiryStatus.Converted
                    && i.Status != InquiryStatus.Rejected
                    && !i.IsDeleted)
                .OrderBy(i => i.Priority)
                .ThenByDescending(i => i.InquiryDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Inquiry>> GetRecentAsync(int count)
        {
            return await _context.Inquiries
                .Include(i => i.AssignedToProgrammer)
                .Where(i => !i.IsDeleted)
                .OrderByDescending(i => i.InquiryDate)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Dictionary<InquiryStatus, int>> GetCountByStatusAsync()
        {
            return await _context.Inquiries
                .Where(i => !i.IsDeleted && i.Status != null)
                .GroupBy(i => i.Status!.Value)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count);
        }
    }
}
