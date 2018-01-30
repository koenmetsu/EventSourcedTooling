namespace EventSourcedTooling
{
    public class CustomerStartedShoppingHandler : ICommandHandler<StartShopping>
    {
        public IEvent Handle(StartShopping command)
        {
            return new CustomerStartedShopping(command.CustomerId, command.CartId);
        }
    }
}