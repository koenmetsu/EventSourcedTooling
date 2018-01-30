namespace EventSourcedTooling
{
    public class ShoppingCartAggregate : ICommandHandler<StartShopping>, ICommandHandler<AddProductToCart>, ICommandHandler<PlaceOrder>
    {
        public object Handle(StartShopping command)
        {
            return new CustomerStartedShopping(command.CustomerId, command.CartId);
        }

        public object Handle(AddProductToCart command)
        {
            throw new System.NotImplementedException();
        }

        public object Handle(PlaceOrder command)
        {
            throw new System.NotImplementedException();
        }
    }
}