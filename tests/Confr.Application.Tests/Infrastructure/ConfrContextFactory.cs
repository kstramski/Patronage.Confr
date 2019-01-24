using System;
using Microsoft.EntityFrameworkCore;
using Confr.Domain.Entities;
using Confr.Persistence;

namespace Confr.Application.Tests.Infrastructure
{
    public class ConfrContextFactory
    {
        public static ConfrDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ConfrDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ConfrDbContext(options);

            context.Database.EnsureCreated();

            context.Rooms.AddRange(new[] {
                new Room { Id = 5, Name = "Room 5" },
                new Room { Id = 7, Name = "Room 7" },
                new Room { Id = 12, Name = "Room 12" },
                new Room { Id = 14, Name = "Room 14" },
            });

            context.RoomReservations.AddRange(new[] {
                new RoomReservation { RoomId = 5, Date =  new DateTime(2019, 01, 20)},
                new RoomReservation { RoomId = 5, Date = new DateTime(2019, 01, 22) },
                new RoomReservation { RoomId = 5, Date = new DateTime(2019, 01, 27) },
                new RoomReservation { RoomId = 14, Date = new DateTime(2019, 01, 24) }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(ConfrDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}