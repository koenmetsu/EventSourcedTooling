using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

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
        private readonly IEvent[] _history;
        private readonly ICommand _command;
        private readonly IEvent _expectedEvent;

        public HtmlReporter(IEvent[] history, ICommand command, IEvent expectedEvent)
        {
            _history = history;
            _command = command;
            _expectedEvent = expectedEvent;
        }

        public void Report()
        {
            var builder = new StringBuilder();

            foreach (var @event in _history)
            {
                builder.AppendLine("<div class=\"event sticky like-paper\">");
                builder.AppendLine($"<h2> {@event.GetType().FullName}</h2>");
                GenerateHtmlProperties(builder, @event);
                builder.AppendLine("</div>");
            }

            builder.AppendLine("<div class=\"command sticky like-paper\">");
            builder.AppendLine($"<h2> {_command.GetType().FullName}</h2>");
            GenerateHtmlProperties(builder, _command);
            builder.AppendLine("</div>");

            builder.AppendLine("<div class=\"event sticky like-paper\">");
            builder.AppendLine($"<h2> {_expectedEvent.GetType().FullName}</h2>");
            GenerateHtmlProperties(builder, _expectedEvent);
            builder.AppendLine("</div>");


            var template = File.ReadAllText("template.html");
            var report = template.Replace("{{placeholder}}", builder.ToString());

            if (!Directory.Exists("Reports"))
                Directory.CreateDirectory("Reports");

            File.WriteAllText(Path.Combine("Reports", DateTime.Now.Ticks.ToString()) + ".html", report);
        }

        private void GenerateHtmlProperties(StringBuilder builder, object command)
        {
            builder.AppendLine($"<table>");
            command.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(command).ToString()).ToList().ForEach(
                pair => { builder.AppendLine($"<tr class='{@pair.Key}'><th>{@pair.Key}</th><td>{@pair.Value}</td></tr>"); });
            builder.AppendLine($"</table>");
        }
    }
}