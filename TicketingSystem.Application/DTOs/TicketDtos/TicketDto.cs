using TicketingSystem.Core.Enums;

namespace TicketingSystem.Application.DTOs.TicketDtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string? TicketNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // Relation Data
        public string? CustomerName { get; set; }
        public string? TagName { get; set; }
        public string? AssignedToUserName { get; set; }
        public string? AreaName { get; set; }
        public string? CityName { get; set; }
        public string? CountryName { get; set; }
    }
}
