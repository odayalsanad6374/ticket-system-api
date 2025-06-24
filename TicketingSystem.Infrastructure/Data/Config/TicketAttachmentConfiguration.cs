using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketingSystem.Core.Entities;

namespace TicketingSystem.Infrastructure.Data.Config
{
    internal class TicketAttachmentConfiguration : IEntityTypeConfiguration<TicketAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketAttachment> builder)
        {
            //table configuration
            builder.ToTable(nameof(TicketAttachment));
            builder.HasKey(x => x.Id);

            //property configuration
            builder.Property(x => x.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.FileName)
                .HasColumnType("nvarchar")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.FilePath)
                .HasColumnType("nvarchar")
                .IsRequired();

            //relation
            builder.HasOne(x => x.Ticket)
                .WithMany(y => y.Attachments)
                .HasForeignKey(x => x.TicketId)
                .IsRequired();

        }
    }
}
