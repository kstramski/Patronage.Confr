using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Confr.Application.Exceptions;
using Confr.Application.Rooms.Commands.CreateRoom;
using Confr.Application.Tests.Infrastructure;
using Confr.Persistence;
using Xunit;

namespace Confr.Application.Tests.Rooms.Commands
{
    [Collection("CommandCollection")]
    public class CreateRoomCommandHandlerTests
    {
        private readonly ConfrDbContext _context;
        private readonly IMediator _mediator;

        public CreateRoomCommandHandlerTests(CommandAndQueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task CreateRoom()
        {
            var commandHandler = new CreateRoomCommandHandler(_context, _mediator);

            var result = await commandHandler.Handle(
                new CreateRoomCommand
                {
                    Id = 20,
                    Name = "Room 20"
                },
                CancellationToken.None);

            Assert.IsType<Unit>(result);
        }

        [Fact]
        public async Task TryToCreateDuplicateOfRoom()
        {
            var commandHandler = new CreateRoomCommandHandler(_context, _mediator);

            await commandHandler.Handle(
                new CreateRoomCommand
                {
                    Id = 21,
                    Name = "Room 21"
                },
                CancellationToken.None);

            var exception = await Assert.ThrowsAsync<CreateFailureException>(() =>
                commandHandler.Handle(new CreateRoomCommand
                    {
                        Id = 21,
                        Name = "Room 21"
                    }, CancellationToken.None));

            Assert.Equal("Creation of entity \"Room\" (21) failed. Room nr 21 already exists.", exception.Message);
        }
    }
}
