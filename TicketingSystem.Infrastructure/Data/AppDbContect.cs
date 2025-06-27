using Microsoft.EntityFrameworkCore;
using System;
using TicketingSystem.Core.Entities;
using TicketingSystem.Infrastructure.Data.Config;

namespace TicketingSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; } 
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
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
