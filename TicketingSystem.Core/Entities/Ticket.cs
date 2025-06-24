using TicketingSystem.Core.Enums;

namespace TicketingSystem.Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? TicketNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.New;
        public TicketPriority Priority { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public int CustomerId { get; set; }// Foreign Key
        public int? AssignedToUserId { get; set; }// Foreign Key
        public int TagId { get; set; }// Foreign Key
        public Customer? Customer { get; set; }
        public User? AssignedUser { get; set; }
        public Tag? Tag { get; set; }
        public ICollection<TicketAttachment>? Attachments { get; set; }// Navigation
    }
}
