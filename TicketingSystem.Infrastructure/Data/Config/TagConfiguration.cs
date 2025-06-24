using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //table configuration
            builder.ToTable(nameof(Tag));
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Name).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
