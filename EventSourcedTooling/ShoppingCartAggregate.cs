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
            return new ProductWasAddedToCart("customerId", command.CartId, command.SKU, command.Price, command.AddTime);
        }

        public IEvent Handle(PlaceOrder command)
        {
            throw new System.NotImplementedException();
        }
    }
}