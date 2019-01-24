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
                .ForMember(rc => rc.Id, opt => opt.MapFrom(r => r.RoomId))
                .ForMember(rc => rc.Calendar, opt => opt.MapFrom(r => r.Calendar.Select(c => c.ReservationDate)));

            //Room => RoomModel
            configuration.CreateMap<Room, RoomModel>()
                .ForMember(rDto => rDto.Id, opt => opt.MapFrom(c => c.RoomId))
                .ForMember(rDto => rDto.Name, opt => opt.MapFrom(c => c.RoomName));


            // UpdateRoomCommand => Room
            configuration.CreateMap<UpdateRoomCommand, Room>()
                .ForMember(r => r.RoomId, opt => opt.MapFrom(urc => urc.Id))
                .ForMember(r => r.RoomName, opt => opt.MapFrom(urc => urc.Name))
                .ForMember(r => r.Calendar, opt => opt.Ignore())
                .AfterMap((urc, r) =>
                {
                    var uniqueReservationsListFromCommand = urc.Calendar
                        .Select(c => c.Date)
                        .Distinct()
                        .ToList();

                    var removedReservations = r.Calendar
                        .Where(c => !uniqueReservationsListFromCommand
                            .Contains(c.ReservationDate))
                        .ToList();

                    foreach (var reservation in removedReservations)
                    {
                        r.Calendar.Remove(reservation);
                    }

                    var addReservation = uniqueReservationsListFromCommand
                        .Where(date => r.Calendar
                            .All(c => c.ReservationDate.Date != date.Date))
                        .Select(date => new RoomReservation
                        {
                            RoomId = r.RoomId,
                            ReservationDate = date.Date
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
