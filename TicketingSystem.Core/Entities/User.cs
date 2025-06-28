
namespace TicketingSystem.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }// Foreign Key
        public Role? Role { get; set; } = null!;// Navigation Property
        public ICollection<Ticket>? AssignedTickets { get; set; }
    }
}
