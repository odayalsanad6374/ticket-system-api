using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            //table configuration
            builder.ToTable(nameof(Ticket));
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.TicketNumber)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Title)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(100);

            builder.Property(x => x.Status).IsRequired();

            builder.Property(x => x.Priority).IsRequired();

            builder.Property(t => t.CreateDate).IsRequired();

            builder.Property(t => t.UpdateDate).IsRequired(false);

            //relation
            builder.HasOne(x => x.Customer)
                .WithMany(y => y.Tickets)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();
            
            builder.HasOne(x => x.AssignedUser)
                .WithMany(y => y.AssignedTickets)
                .HasForeignKey(r => r.AssignedToUserId)
                .IsRequired(false);

            builder.HasOne(x => x.Tag)
                .WithMany(y => y.Tickets)
                .HasForeignKey(x => x.TagId)
                .IsRequired();
        }
    }
}
