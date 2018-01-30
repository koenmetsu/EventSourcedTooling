namespace EventSourcedTooling
{
    public class ShoppingCartAggregate : ICommandHandler<StartShopping>, ICommandHandler<AddProductToCart>, ICommandHandler<PlaceOrder>
    {
        public IEvent Handle(StartShopping command)
        {
            return new CustomerStartedShopping(command.CustomerId, command.CartId);
        }

        public IEvent Handle(AddProductToCart command)
        {
            throw new System.NotImplementedException();
        }

        public IEvent Handle(PlaceOrder command)
        {
            throw new System.NotImplementedException();
        }
    }
}