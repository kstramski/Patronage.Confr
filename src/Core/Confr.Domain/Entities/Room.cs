using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Confr.Domain.Entities
{
    public class Room
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public ICollection<RoomReservation> Calendar { get; private set; }

        public Room()
        {
            Calendar = new Collection<RoomReservation>();
        }
    }
}
