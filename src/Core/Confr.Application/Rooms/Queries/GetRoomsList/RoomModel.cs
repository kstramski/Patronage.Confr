using AutoMapper;
using Confr.Application.Interfaces.Mapping;
using Confr.Domain.Entities;

namespace Confr.Application.Rooms.Queries.GetRoomsList
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
