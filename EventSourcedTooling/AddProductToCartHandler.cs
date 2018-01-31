namespace EventSourcedTooling
{
    public class AddProductToCartHandler : ICommandHandler<AddProductToCart>
    {
        public IEvent Handle(AddProductToCart command)
        {
            return new ShoppingCartAggregate().Handle(command);
        }
    }
}