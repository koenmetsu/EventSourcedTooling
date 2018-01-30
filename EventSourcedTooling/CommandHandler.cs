namespace EventSourcedTooling
{
    public interface ICommandHandler<TCommand>
    {
        object Handle(TCommand command);
    }
}