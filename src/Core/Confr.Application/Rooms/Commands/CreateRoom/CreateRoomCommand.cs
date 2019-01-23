using MediatR;

namespace Confr.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
