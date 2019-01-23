using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Confr.Application.Exceptions;
using Confr.Domain.Entities;
using Confr.Persistence;

namespace Confr.Application.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand,Unit>
    {
        private readonly ConfrDbContext _context;
        private readonly IMediator _mediator;

        public CreateRoomCommandHandler(
            ConfrDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rooms.FindAsync(request.Id);

            if (entity != null)
            {
                throw new CreateFailureException(nameof(Room), request.Id, $"Room nr {request.Id} already exists.");
            }

            entity = new Room
            {
                RoomId = request.Id,
                RoomName = request.Name
            };

            _context.Rooms.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new RoomCommandNotification
            {
                Subject = "Room created",
                Body = $"Room {request.Id} has been created."
            });

            return Unit.Value;
        }
    }
}
