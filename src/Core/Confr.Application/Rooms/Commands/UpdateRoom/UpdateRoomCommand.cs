using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using Confr.Application.Interfaces.Mapping;
using Confr.Domain.Entities;

namespace Confr.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommand : IRequest, IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<DateTime> Calendar { get; set; }

        public UpdateRoomCommand()
        {
            Calendar = new List<DateTime>();
        }

        public void CreateMappings(Profile configuration)
        {
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
