using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Confr.Domain.Entities;

namespace Confr.Persistence.Configurations
{
    public class RoomReservationConfiguration : IEntityTypeConfiguration<RoomReservation>
    {
        public void Configure(EntityTypeBuilder<RoomReservation> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("ID");

            builder.Property(e => e.RoomId).HasColumnName("RoomID");

            builder.HasOne(c => c.Room)
                .WithMany(r => r.Calendar)
                .HasForeignKey(c => c.RoomId);


        }
    }
}
