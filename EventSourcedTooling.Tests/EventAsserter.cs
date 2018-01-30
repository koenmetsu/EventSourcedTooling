using Xunit;

namespace EventSourcedTooling.Tests
{
    public interface IEventAsserter
    {
        void Then(IEvent actual);
    }

    public class EventAsserter<TCommand> : IEventAsserter
    {
        private readonly ICommandHandler<TCommand> handler;
        private IEvent _event;

        public EventAsserter(ICommandHandler<TCommand> handler)
        {
            this.handler = handler;
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