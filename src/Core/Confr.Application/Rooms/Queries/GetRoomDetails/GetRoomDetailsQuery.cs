using MediatR;

namespace Confr.Application.Rooms.Queries.GetRoomDetails
{
    public class GetRoomDetailsQuery : IRequest<RoomDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
