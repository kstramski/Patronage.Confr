using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Confr.Application.Exceptions;
using Confr.Domain.Entities;
using Confr.Persistence;

namespace Confr.Application.Rooms.Queries.GetRoomCalendar
{
    public class GetRoomCalendarQueryHandler : IRequestHandler<GetRoomCalendarQuery, RoomCalendarViewModel>
    {
        private readonly ConfrDbContext _context;

        public GetRoomCalendarQueryHandler(ConfrDbContext context)
        {
            _context = context;
        }

        public async Task<RoomCalendarViewModel> Handle(GetRoomCalendarQuery request, CancellationToken cancellationToken)
        {
            var roomEntity = await _context.Rooms.Include(r => r.Calendar).SingleOrDefaultAsync(r => r.RoomId == request.Id);

            if (roomEntity == null)
            {
                throw new NotFoundException(nameof(Room), request.Id);
            }

            var calendarList = new List<DateTime>();

            foreach (var calendar in roomEntity.Calendar)
            {
                calendarList.Add(calendar.ReservationDate);
            }

            calendarList.Sort();

            return new RoomCalendarViewModel
            {
                Id = request.Id,
                Calendar = calendarList
            };
        }
    }
}
