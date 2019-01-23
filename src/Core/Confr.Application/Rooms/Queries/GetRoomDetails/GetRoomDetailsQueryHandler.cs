using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Confr.Application.Exceptions;
using Confr.Domain.Entities;
using Confr.Persistence;

namespace Confr.Application.Rooms.Queries.GetRoomDetails
{
    public class GetRoomDetailsQueryHandler :IRequestHandler<GetRoomDetailsQuery, RoomDetailsViewModel>
    {
        private readonly ConfrDbContext _context;

        public GetRoomDetailsQueryHandler(ConfrDbContext context)
        {
            _context = context;
        }

        public async Task<RoomDetailsViewModel> Handle(GetRoomDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Rooms.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Room), request.Id);
            }

            return new RoomDetailsViewModel
            {
                Id = entity.RoomId,
                Name = entity.RoomName
            };
        }
    }
}
