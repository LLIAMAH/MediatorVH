using MediatorVH.Interfaces;
using MediatorVH.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace MediatorVH.Tests
{
    public class CommandTests
    {
        [Fact]
        public async Task Registers_Command_Handler_By_Type()
        {
            var services = new ServiceCollection();

            // register by providing the handler type to the extension
            services.AddMediatorVh([typeof(TestCommandHandler)]);
            var provider = services.BuildServiceProvider();
            var handler = provider.GetService<ICommandHandler<TestCommand>>();
            Assert.NotNull(handler);
            Assert.IsType<TestCommandHandler>(handler);
            bool? thereIsNoException = null;
            try
            {
                await handler.Handle(new TestCommand(), CancellationToken.None);
                thereIsNoException = true;
            }
            catch
            {
                thereIsNoException = false;
            }
            Assert.True(thereIsNoException.Value);
        }

        [Fact]
        public async Task Registers_Command_With_Response_Handler_By_Type()
        {
            var services = new ServiceCollection();

            services.AddMediatorVh([typeof(TestCommandWithResponseHandler)]);
            var provider = services.BuildServiceProvider();
            var handler = provider.GetService<ICommandHandler<TestCommandWithResponse, int>>();
            Assert.NotNull(handler);
            Assert.IsType<TestCommandWithResponseHandler>(handler);
            var response = await handler.Handle(new TestCommandWithResponse(), CancellationToken.None);
            Assert.Equal(42, response);
        }

        // sample command and handlers used only for tests
        // ReSharper disable once ClassNeverInstantiated.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public record TestCommand() : ICommand;

        private class TestCommandHandler : ICommandHandler<TestCommand>
        {
            public Task Handle(TestCommand command, CancellationToken cancellationToken) => Task.CompletedTask;
        }

        // ReSharper disable once ClassNeverInstantiated.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public record TestCommandWithResponse() : ICommand<int>;

        private class TestCommandWithResponseHandler : ICommandHandler<TestCommandWithResponse, int>
        {
            public Task<int> Handle(TestCommandWithResponse command, CancellationToken cancellationToken) => Task.FromResult(42);
        }
    }
}
