using System;
using System.Collections.Generic;

namespace Confr.Application.Rooms.Queries.GetRoomCalendar
{
    public class RoomCalendarViewModel
    {
        public int Id { get; set; }

        public IEnumerable<DateTime> Calendar { get; set; }
    }
}
