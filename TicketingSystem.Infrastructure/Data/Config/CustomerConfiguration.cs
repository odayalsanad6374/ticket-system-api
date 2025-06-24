using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            //table configuration
            builder.ToTable(nameof(Customer));
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Name).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

           builder.Property(x => x.Email).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

           builder.Property(x => x.PhoneNumber).HasColumnType("nvarchar")
                 .HasMaxLength(100)
                 .IsRequired();
           builder.HasOne(c => c.Area)
                 .WithMany(v => v.Customers)
                 .HasForeignKey(x => x.AreaId)
                 .IsRequired();

        }
    }
}
