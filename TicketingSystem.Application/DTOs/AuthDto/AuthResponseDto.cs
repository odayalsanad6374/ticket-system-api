using TicketingSystem.Application.DTOs.UserDtos;

namespace TicketingSystem.Application.DTOs.AuthDto
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public UserDto? User { get; set; }
    }
}
