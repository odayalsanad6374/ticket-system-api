using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //table configuration
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Name).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
