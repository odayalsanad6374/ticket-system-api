
namespace TicketingSystem.Core.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }
    }
}
