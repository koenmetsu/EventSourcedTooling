using Xunit;

namespace EventSourcedTooling.Tests
{
    public interface IEventAsserter
    {
        void Then(object @event);
    }

    public class EventAsserter<TCommand> : IEventAsserter, ITestCommandExecutor<TCommand>
    {
        private readonly ICommandHandler<TCommand> handler;
        private object _event;
        private object given;

        public EventAsserter(ICommandHandler<TCommand> handler)
        {
            this.handler = handler;
        }

        public IEventAsserter When(TCommand command)
        {
            _event = handler.Handle(command);
            return this;
        }
        
        public void Then(object @event)
        {
            Assert.Equal(@event, _event);
        }

        public ITestCommandExecutor<TCommand> Given(object @event)
        {
            given = @event;
            return this;
        }
    }

    public interface ITestCommandExecutor<TCommand>
    {
        IEventAsserter When(TCommand command);
    }
}