using System.Threading;
using System.Threading.Tasks;
using Confr.Application.Exceptions;
using Confr.Application.Rooms.Queries.GetRoomDetails;
using Confr.Application.Tests.Infrastructure;
using Confr.Persistence;
using Xunit;

namespace Confr.Application.Tests.Rooms.Queries
{
    [Collection("QueryCollection")]
    public class GetRoomDetailsQueryHandlerTests
    {
        private readonly ConfrDbContext _context;

        public GetRoomDetailsQueryHandlerTests(CommandAndQueryTestFixture fixture)
        {
            _context = fixture.Context;
        }

        [Fact]
        public async Task GetRoomDetails()
        {
            var queryHandler = new GetRoomDetailsQueryHandler(_context);

            var result = await queryHandler.Handle(new GetRoomDetailsQuery{ Id = 5 }, CancellationToken.None);

            Assert.IsType<RoomDetailsViewModel>(result);
            Assert.Equal(5, result.Id);
            Assert.Equal("Room 5", result.Name);
        }

        [Fact]
        public async Task NotFoundRoomDetails()
        {
            var queryHandler = new GetRoomDetailsQueryHandler(_context);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                 queryHandler.Handle(new GetRoomDetailsQuery { Id = 1 }, CancellationToken.None));

            Assert.Equal("Entity \"Room\" (1) was not found.", exception.Message);
        }
    }
}
