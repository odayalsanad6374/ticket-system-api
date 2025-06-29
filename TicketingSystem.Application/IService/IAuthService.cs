using TicketingSystem.Application.DTOs.AuthDto;
using TicketingSystem.Application.DTOs.LoginDto;

namespace TicketingSystem.Application.IService
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    }
}
