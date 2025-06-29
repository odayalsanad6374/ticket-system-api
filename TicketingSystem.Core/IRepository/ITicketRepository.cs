using TicketingSystem.Core.Entities;

namespace TicketingSystem.Core.IRepository
{
    public interface ITicketRepository: IRepository<Ticket>
    {
        IQueryable<Ticket> Query();
    }
}
