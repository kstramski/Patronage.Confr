using System;
using AutoMapper;
using MediatR;
using Moq;
using Confr.Persistence;
using Xunit;

namespace Confr.Application.Tests.Infrastructure
{
    public class CommandAndQueryTestFixture : IDisposable
    {
        public ConfrDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        public IMediator Mediator  { get; private set; }

        public CommandAndQueryTestFixture()
        {
            Context = ConfrContextFactory.Create();
            Mapper = AutoMapperFactory.Create();
            Mediator = new Mock<IMediator>().Object;
        }

        public void Dispose()
        {
            ConfrContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<CommandAndQueryTestFixture> { }

    [CollectionDefinition("CommandCollection")]
    public class CommandCollection : ICollectionFixture<CommandAndQueryTestFixture> { }
}