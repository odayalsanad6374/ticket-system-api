
namespace TicketingSystem.Core.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<Area> Areas { get; set;} = new List<Area>();
    }
}
