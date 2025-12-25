using CRM.BLL.Common;
using CRM.BLL.DTOs.AuthDTOs;
using CRM.BLL.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<IdentityUser> userManager,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login(
            [FromBody] LoginRequestDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponse<LoginResponseDto>.Failure(errors));
                }

                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                {
                    return Unauthorized(ApiResponse<LoginResponseDto>.Failure(
                        "البريد الإلكتروني أو كلمة المرور غير صحيحة"));
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (!isPasswordValid)
                {
                    return Unauthorized(ApiResponse<LoginResponseDto>.Failure(
                        "البريد الإلكتروني أو كلمة المرور غير صحيحة"));
                }

                var token = await _tokenService.GenerateTokenAsync(user, _userManager);
                var expiryDays = int.Parse(_configuration["JWT:ExpiryInDays"] ?? "7");

                var response = new LoginResponseDto
                {
                    Token = token,
                    Email = user.Email!,
                    UserName = user.UserName!,
                    ExpiresAt = DateTime.UtcNow.AddDays(expiryDays)
                };

                return Ok(ApiResponse<LoginResponseDto>.Success(response, "تم تسجيل الدخول بنجاح"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<LoginResponseDto>.Failure(
                    $"حدث خطأ: {ex.Message}"));
            }
        }

        [HttpGet("validate")]
        [Authorize]
        public ActionResult<ApiResponse<object>> ValidateToken()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var userName = User.FindFirst("userName")?.Value;

            return Ok(ApiResponse<object>.Success(new
            {
                IsValid = true,
                Email = email,
                UserName = userName
            }));
        }

        [HttpPost("logout")]
        [Authorize]
        public ActionResult<ApiResponse<bool>> Logout()
        {
            // For JWT, logout is handled on client side by removing token
            return Ok(ApiResponse<bool>.Success(true, "تم تسجيل الخروج بنجاح"));
        }
    }
}

