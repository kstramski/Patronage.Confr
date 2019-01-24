using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Confr.Application.Exceptions;
using Confr.Application.Rooms.Queries.GetRoomCalendar;
using Confr.Application.Tests.Infrastructure;
using Confr.Persistence;
using Xunit;

namespace Confr.Application.Tests.Rooms.Queries
{
    [Collection("QueryCollection")]
    public class GetRoomCalendarQueryHandlerTests
    {
        private readonly ConfrDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomCalendarQueryHandlerTests(CommandAndQueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetRoomCalendarWithReservations()
        {
            var queryHandler = new GetRoomCalendarQueryHandler(_context, _mapper);

            var result = await queryHandler.Handle(new GetRoomCalendarQuery{ Id = 5 }, CancellationToken.None);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Equal(5, result.Id);
            Assert.Equal(3, result.Calendar.Count());

        }

        [Fact]
        public async Task GetRoomCalendarWithoutReservations()
        {
            var queryHandler = new GetRoomCalendarQueryHandler(_context, _mapper);

            var result = await queryHandler.Handle(new GetRoomCalendarQuery { Id = 7 }, CancellationToken.None);

            Assert.IsType<RoomCalendarViewModel>(result);
            Assert.Equal(7, result.Id);
            Assert.Empty(result.Calendar);

        }

        [Fact]
        public async Task NotFoundRoomCalendar()
        {
            var queryHandler = new GetRoomCalendarQueryHandler(_context, _mapper);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                queryHandler.Handle(new GetRoomCalendarQuery { Id = 1 }, CancellationToken.None));

            Assert.Equal("Entity \"Room\" (1) was not found.", exception.Message);
        }
    }
}
