using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;
using TicketingSystem.Infrastructure.Data;

namespace TicketingSystem.Infrastructure.Repository
{
    public class TicketRepository: Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }
        public IQueryable<Ticket> Query()
        {
            return _context.Set<Ticket>().AsQueryable();
        }
    }
}
