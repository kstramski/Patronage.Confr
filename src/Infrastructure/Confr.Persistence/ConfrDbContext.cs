using Microsoft.EntityFrameworkCore;
using Confr.Domain.Entities;

namespace Confr.Persistence
{
    public class ConfrDbContext : DbContext
    {
        public ConfrDbContext(DbContextOptions<ConfrDbContext> options)
            : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomReservation> RoomReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConfrDbContext).Assembly);
        }
    }
}
