using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Confr.Application.Exceptions;
using Confr.Domain.Entities;
using Confr.Persistence;

namespace Confr.Application.Rooms.Commands.DeleteRoom
{
    public class DeteleRoomCommandHandler : IRequestHandler<DeleteRoomCommand,Unit>
    {
        private readonly ConfrDbContext _context;
        private readonly IMediator _mediator;

        public DeteleRoomCommandHandler(
            ConfrDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rooms.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Room), request.Id);
            }

            _context.Rooms.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new RoomCommandNotification
            {
                Subject = "Room deleted",
                Body = $"Room {request.Id} has been deleted."
            });

            return Unit.Value;
        }
    }
}
