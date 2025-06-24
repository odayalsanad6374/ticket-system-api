using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //table configuration
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Name).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.PasswordHash).HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired();


            //relation
            builder.HasOne(x => x.Role)
                .WithMany(y => y.Users)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}
