using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            //table configuration
            builder.ToTable(nameof(City));
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Name).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            //relation
            builder.HasOne(x => x.Country)
                .WithMany(y => y.Cities)
                .HasForeignKey(x => x.CountryId)
                .IsRequired();
        }
    }
}
