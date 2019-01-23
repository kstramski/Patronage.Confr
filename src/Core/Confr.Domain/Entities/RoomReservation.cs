using System;

namespace Confr.Domain.Entities
{
    public class RoomReservation
    {
        public int ReservationId { get; set; }

        public int RoomId { get; set; }

        public DateTime ReservationDate { get; set; }

        public Room Room { get; set; }
    }
}
