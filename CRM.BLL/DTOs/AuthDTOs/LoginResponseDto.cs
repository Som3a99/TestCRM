using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.DTOs.AuthDTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}
