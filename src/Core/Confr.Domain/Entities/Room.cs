using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Confr.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RoomReservation> Calendar { get; private set; }

        public Room()
        {
            Calendar = new Collection<RoomReservation>();
        }
    }
}
