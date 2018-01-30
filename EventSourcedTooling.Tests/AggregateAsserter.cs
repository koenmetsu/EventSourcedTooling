using Xunit;

namespace EventSourcedTooling.Tests
{
    public interface IEventAsserter
    {
        void Then(IEvent @event);
    }

    public interface ITestCommandExecutor<TCommand> where TCommand: ICommand
    {
        IEventAsserter When(TCommand command);
    }

    public class AggregateAsserter<TCommand> : IEventAsserter, ITestCommandExecutor<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> handler;
        private IEvent _event;
        private IEvent[] given;

        public AggregateAsserter(ICommandHandler<TCommand> handler)
        {
            this.handler = handler;
        }

        public ITestCommandExecutor<TCommand> Given(params IEvent[] @event)
        {
            given = @event;
            return this;
        }

        public IEventAsserter When(TCommand command)
        {
            _event = handler.Handle(command);
            return this;
        }

        public void Then(IEvent @event)
        {
            Assert.Equal(@event, _event);
        }
    }
}