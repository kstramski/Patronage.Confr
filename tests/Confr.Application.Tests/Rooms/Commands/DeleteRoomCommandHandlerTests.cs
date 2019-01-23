using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Confr.Application.Exceptions;
using Confr.Application.Rooms.Commands.DeleteRoom;
using Confr.Application.Tests.Infrastructure;
using Confr.Persistence;
using Xunit;

namespace Confr.Application.Tests.Rooms.Commands
{
    [Collection("CommandCollection")]
    public class DeleteRoomCommandHandlerTests
    {
        private readonly ConfrDbContext _context;
        private readonly IMediator _mediator;

        public DeleteRoomCommandHandlerTests(CommandAndQueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task DeleteRoom()
        {
            var commandHandler = new DeteleRoomCommandHandler(_context, _mediator);

            var result = await commandHandler.Handle(new DeleteRoomCommand{ Id = 12 }, CancellationToken.None);

            Assert.IsType<Unit>(result);
        }

        [Fact]
        public async Task NotFoundRoomToDelete()
        {
            var commandHandler = new DeteleRoomCommandHandler(_context, _mediator);
            
            var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
                commandHandler.Handle(new DeleteRoomCommand { Id = 50 }, CancellationToken.None));

            Assert.Equal("Entity \"Room\" (50) was not found.", exception.Message);
        }
    }
}
