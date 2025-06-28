using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Application.DTOs.AuthDto;
using TicketingSystem.Application.DTOs.LoginDto;
using TicketingSystem.Application.IService;

namespace TicketingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(token);
            }
            catch (Exception e)
            {
                return Unauthorized(new { message = e.Message });
            }
        }
    }
}
