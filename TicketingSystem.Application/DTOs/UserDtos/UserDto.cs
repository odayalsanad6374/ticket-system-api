
namespace TicketingSystem.Application.DTOs.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int RoleId { get; set; }
    }
}