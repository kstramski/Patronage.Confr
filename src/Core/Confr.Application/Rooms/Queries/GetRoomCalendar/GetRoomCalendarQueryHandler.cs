using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetRoomCalendarQueryHandler(ConfrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomCalendarViewModel> Handle(GetRoomCalendarQuery request, CancellationToken cancellationToken)
        {
            var roomEntity = await _context.Rooms
                .Include(r => r.Calendar)
                .SingleOrDefaultAsync(r => r.RoomId == request.Id);

            if (roomEntity == null)
            {
                throw new NotFoundException(nameof(Room), request.Id);
            }

            return _mapper.Map<Room, RoomCalendarViewModel>(roomEntity);
        }
    }
}
