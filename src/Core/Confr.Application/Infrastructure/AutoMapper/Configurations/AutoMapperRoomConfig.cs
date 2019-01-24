using System.Linq;
using AutoMapper;
using Confr.Application.Interfaces.Mapping;
using Confr.Application.Rooms.Commands.UpdateRoom;
using Confr.Application.Rooms.Queries.GetRoomCalendar;
using Confr.Application.Rooms.Queries.GetRoomsList;
using Confr.Domain.Entities;

namespace Confr.Application.Infrastructure.AutoMapper.Configurations
{
    public class AutoMapperRoomConfig : IHaveCustomMapping
    {
        public void CreateMappings(Profile configuration)
        {
            // Room => RoomCalendarViewModel
            configuration.CreateMap<Room, RoomCalendarViewModel>()
                .ForMember(rc => rc.Id, opt => opt.MapFrom(r => r.Id))
                .ForMember(rc => rc.Calendar, opt => opt.MapFrom(r => r.Calendar.Select(c => c.Date)));

            //Room => RoomModel
            configuration.CreateMap<Room, RoomModel>()
                .ForMember(rDto => rDto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(rDto => rDto.Name, opt => opt.MapFrom(c => c.Name));


            // UpdateRoomCommand => Room
            configuration.CreateMap<UpdateRoomCommand, Room>()
                .ForMember(r => r.Id, opt => opt.MapFrom(urc => urc.Id))
                .ForMember(r => r.Name, opt => opt.MapFrom(urc => urc.Name))
                .ForMember(r => r.Calendar, opt => opt.Ignore())
                .AfterMap((urc, r) =>
                {
                    var uniqueReservationsListFromCommand = urc.Calendar
                        .Select(c => c.Date)
                        .Distinct()
                        .ToList();

                    var removedReservations = r.Calendar
                        .Where(c => !uniqueReservationsListFromCommand
                            .Contains(c.Date))
                        .ToList();

                    foreach (var reservation in removedReservations)
                    {
                        r.Calendar.Remove(reservation);
                    }

                    var addReservation = uniqueReservationsListFromCommand
                        .Where(date => r.Calendar
                            .All(c => c.Date.Date != date.Date))
                        .Select(date => new RoomReservation
                        {
                            RoomId = r.Id,
                            Date = date.Date
                        })
                        .ToList();

                    foreach (var reservation in addReservation)
                    {
                        r.Calendar.Add(reservation);
                    }
                });
        }
    }
}
