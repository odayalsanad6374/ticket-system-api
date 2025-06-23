using Microsoft.EntityFrameworkCore;
using TicketingSystem.Infrastructure.Data.Config;

namespace TicketingSystem.Infrastructure.Data
{
    public class AppDbContect: DbContext
    {
        public AppDbContect(DbContextOptions<AppDbContect> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketConfiguration).Assembly);
        }
    }
}
