using MediatorVH.Interfaces;
using MediatorVH.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorVH.Tests
{
    public class QueryTests
    {
        [Fact]
        public void Registers_Query_Handler_By_Type()
        {
            var services = new ServiceCollection();

            services.AddMediatorVh([typeof(TestQueryHandler)]);

            var provider = services.BuildServiceProvider();

            var handler = provider.GetService<IQueryHandler<TestQuery, string>>();

            Assert.NotNull(handler);
            Assert.IsType<TestQueryHandler>(handler);
        }

        // sample query and handler used only for tests
        // ReSharper disable once ClassNeverInstantiated.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public record TestQuery() : IQuery<string>;

        private class TestQueryHandler : IQueryHandler<TestQuery, string>
        {
            public Task<string> Handle(TestQuery query, CancellationToken cancellationToken) => Task.FromResult("ok");
        }
    }
}
