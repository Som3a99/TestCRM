namespace CRM.PL.Controllers
{
    public class TokenValidationDto
    {
        public bool IsValid { get; set; }
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
    }
}