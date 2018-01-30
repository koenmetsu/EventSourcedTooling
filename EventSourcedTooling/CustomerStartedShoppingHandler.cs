namespace EventSourcedTooling
{
    public class CustomerStartedShoppingHandler : ICommandHandler<StartShopping>
    {
        public object Handle(StartShopping command)
        {
            //return new CustomerStartedShopping(command.CustomerId, command.CartId);
            return null;
        }
    }
}