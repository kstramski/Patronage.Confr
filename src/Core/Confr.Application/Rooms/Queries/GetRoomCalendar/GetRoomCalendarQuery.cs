using MediatR;

namespace Confr.Application.Rooms.Queries.GetRoomCalendar
{
    public class GetRoomCalendarQuery : IRequest<RoomCalendarViewModel>
    {
        public int Id { get; set; }
    }
}
