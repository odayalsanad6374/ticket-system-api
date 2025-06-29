
namespace TicketingSystem.Application.DTOs.CustomerDtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int AreaId { get; set; }
    }
}