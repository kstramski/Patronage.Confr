using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Confr.Application.Rooms.Queries.GetRoomsList;
using Confr.Application.Tests.Infrastructure;
using Confr.Persistence;
using Xunit;

namespace Confr.Application.Tests.Rooms.Queries
{
    [Collection("QueryCollection")]
    public class GetRoomsListQueryHandlerTests
    {
        private readonly ConfrDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomsListQueryHandlerTests(CommandAndQueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetRoomsListTest()
        {
            var queryHandler = new GetRoomsListQueryHandler(_context, _mapper);

            var result = await queryHandler.Handle(new GetRoomsListQuery(), CancellationToken.None);

            Assert.IsType<RoomsListViewModel>(result);
            Assert.Equal(4, result.Rooms.Count());
        }
    }
}
