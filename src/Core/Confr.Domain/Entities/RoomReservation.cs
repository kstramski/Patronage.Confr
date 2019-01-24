using System;

namespace Confr.Domain.Entities
{
    public class RoomReservation
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public DateTime Date { get; set; }

        public Room Room { get; set; }
    }
}
