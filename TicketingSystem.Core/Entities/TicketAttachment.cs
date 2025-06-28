
namespace TicketingSystem.Core.Entities
{
    public class TicketAttachment
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int TicketId { get; set; } // Foreign Key
        public Ticket Ticket { get; set; } = null!;
    }
}