using Microsoft.AspNetCore.Identity;

namespace CRM.BLL.Interfaces.Services
{
    public interface ITokenService
    {
        Task<string> GenerateTokenAsync(IdentityUser user, UserManager<IdentityUser> userManager);

    }
}
