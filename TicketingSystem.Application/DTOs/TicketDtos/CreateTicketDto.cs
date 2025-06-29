using TicketingSystem.Core.Enums;

namespace TicketingSystem.Application.DTOs.TicketDtos
{
    public class CreateTicketDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TicketPriority Priority { get; set; }
        public int CustomerId { get; set; }
        public int? AssignedToUserId { get; set; }
        public int TagId { get; set; }
    }
}
