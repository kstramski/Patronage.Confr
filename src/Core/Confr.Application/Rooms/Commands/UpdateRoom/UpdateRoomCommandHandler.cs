using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Confr.Application.Exceptions;
using Confr.Domain.Entities;
using Confr.Persistence;

namespace Confr.Application.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand,Unit>
    {
        private readonly ConfrDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateRoomCommandHandler(
            ConfrDbContext context,
            IMapper mapper,
            IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomEntity = await _context.Rooms.Include(r => r.Calendar).SingleOrDefaultAsync(r => r.Id == request.Id);

            if (roomEntity == null)
            {
                throw new NotFoundException(nameof(Room), request.Id);
            }

            request.Calendar = request.Calendar.Distinct().ToList();

            _mapper.Map<UpdateRoomCommand, Room>(request, roomEntity);

            
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new RoomCommandNotification
            {
                Subject = "Room updated",
                Body = $"Room {request.Id} has been updated."
            });

            return Unit.Value;
        }
    }
}
