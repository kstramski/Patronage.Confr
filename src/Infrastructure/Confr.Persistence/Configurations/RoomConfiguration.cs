using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Confr.Domain.Entities;

namespace Confr.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(e => e.RoomId)
                .HasColumnName("RoomID")
                .ValueGeneratedNever();

            builder.Property(e => e.RoomName)
                .HasMaxLength(25)
                .IsRequired();

        }
    }
}
