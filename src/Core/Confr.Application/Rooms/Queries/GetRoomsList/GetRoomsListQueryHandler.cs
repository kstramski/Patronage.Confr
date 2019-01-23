using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Confr.Persistence;

namespace Confr.Application.Rooms.Queries.GetRoomsList
{
    public class GetRoomsListQueryHandler : IRequestHandler<GetRoomsListQuery, RoomsListViewModel>
    {
        private readonly ConfrDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomsListQueryHandler(
            ConfrDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomsListViewModel> Handle(GetRoomsListQuery request, CancellationToken cancellationToken)
        {
            return new RoomsListViewModel
            {
                Rooms = await _context.Rooms.ProjectTo<RoomModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
            };
        }
    }
}
