using TicketingSystem.Core.Entities;
using TicketingSystem.Core.IRepository;
using TicketingSystem.Infrastructure.Data;

namespace TicketingSystem.Infrastructure.Repository
{
    public class TicketRepository: Repository<Ticket>, ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Ticket> Query()
        {
            return _context.Tickets.AsQueryable();
        }
    }
}
