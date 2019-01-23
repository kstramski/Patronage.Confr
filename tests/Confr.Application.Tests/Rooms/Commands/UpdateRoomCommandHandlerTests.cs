using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Confr.Application.Rooms.Commands.UpdateRoom;
using Confr.Application.Rooms.Queries.GetRoomCalendar;
using Confr.Application.Tests.Infrastructure;
using Confr.Persistence;
using Xunit;

namespace Confr.Application.Tests.Rooms.Commands
{
    [Collection("CommandCollection")]
    public class UpdateRoomCommandHandlerTests
    {
        private readonly ConfrDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateRoomCommandHandlerTests(CommandAndQueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task AddReservationToRoom()
        {
            var commandHandler = new UpdateRoomCommandHandler(_context, _mapper, _mediator);

            var result = await GetRoomReservations(7);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Empty(result.Calendar);

            var reservationsList = result.Calendar.ToList();
            reservationsList.Add(new DateTime(2019, 02, 01));
            reservationsList.Add(new DateTime(2019, 02, 05));

            await commandHandler.Handle(
                new UpdateRoomCommand
                {
                    Id = result.Id,
                    Name = $"Room {result.Id}",
                    Calendar = reservationsList
                },
                CancellationToken.None);


            result = await GetRoomReservations(result.Id);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Equal(2, result.Calendar.Count());
        }

        [Fact]
        public async Task RemoveReservationFromRoom()
        {
            var commandHandler = new UpdateRoomCommandHandler(_context, _mapper, _mediator);

            var result = await GetRoomReservations(5);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Equal(3, result.Calendar.Count());

            var reservationsList = result.Calendar.ToList();
            reservationsList.RemoveAt(0);

            await commandHandler.Handle(
                new UpdateRoomCommand
                {
                    Id = result.Id,
                    Name = $"Room {result.Id}",
                    Calendar = reservationsList
                },
                CancellationToken.None);


            result = await GetRoomReservations(result.Id);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Equal(2, result.Calendar.Count());

        }

        [Fact]
        public async Task RemoveAllReservationFromRoom()
        {
            var commandHandler = new UpdateRoomCommandHandler(_context, _mapper, _mediator);

            var result = await GetRoomReservations(14);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Single(result.Calendar);


            await commandHandler.Handle(
                new UpdateRoomCommand
                {
                    Id = result.Id,
                    Name = $"Room {result.Id}",
                    Calendar = new List<DateTime>()
                },
                CancellationToken.None);


            result = await GetRoomReservations(result.Id);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Empty(result.Calendar);


        }

        private async Task<RoomCalendarViewModel> GetRoomReservations(int id)
        {
            var queryHandler = new GetRoomCalendarQueryHandler(_context);

            return await queryHandler.Handle(new GetRoomCalendarQuery { Id = id }, CancellationToken.None);
        }

    }
}
