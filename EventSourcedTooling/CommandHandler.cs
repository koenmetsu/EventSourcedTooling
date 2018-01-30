namespace EventSourcedTooling
{
    public interface ICommandHandler<TCommand>
    {
        IEvent Handle(TCommand command);
    }
}