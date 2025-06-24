using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            //table configuration
            builder.ToTable(nameof(Area));
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Name).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            //relation
            builder.HasOne(x => x.City)
                .WithMany(y => y.Areas)
                .HasForeignKey(x => x.CityId)
                .IsRequired();
        }
    }
}
