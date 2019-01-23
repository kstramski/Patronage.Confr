using AutoMapper;
using Confr.Application.Interfaces.Mapping;
using Confr.Domain.Entities;

namespace Confr.Application.Rooms.Queries.GetRoomsList
{
    public class RoomModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Room, RoomModel>()
                .ForMember(rDto => rDto.Id, opt => opt.MapFrom(c => c.RoomId))
                .ForMember(rDto => rDto.Name, opt => opt.MapFrom(c => c.RoomName));
        }
    }
}
