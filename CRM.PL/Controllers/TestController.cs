using CRM.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRM.PL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("test-db")]
        public async Task<IActionResult> TestDatabase()
        {
            // Test: Get all technologies
            var techs = await _context.Technologies
                .Where(t => !t.IsDeleted)
                .ToListAsync();

            return Ok(new
            {
                Count = techs.Count,
                Technologies = techs
            });
        }
    }
}
