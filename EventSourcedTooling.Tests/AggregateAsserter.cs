﻿using Xunit;

namespace EventSourcedTooling.Tests
{
    public interface IEventAsserter
    {
        void Then(IEvent expectedEvent);
    }

    public interface ITestCommandExecutor<TCommand> where TCommand: ICommand
    {
        IEventAsserter When(TCommand command);
    }

    public class AggregateAsserter<TCommand> : IEventAsserter, ITestCommandExecutor<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> handler;
        private IEvent[] given;
        private TCommand command;

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
            this.command = command;
            return this;
        }

        public void Then(IEvent expectedEvent)
        {
            new HtmlReporter(given, command, expectedEvent).Report();

            var actualEvent = handler.Handle(command);
            Assert.Equal(expectedEvent, actualEvent);
        }
    }

    public class HtmlReporter
    {
        public HtmlReporter(IEvent[] history, ICommand command, IEvent expectedEvent)
        {
            throw new System.NotImplementedException();
        }

        public void Report()
        {
            throw new System.NotImplementedException();
        }
    }
}