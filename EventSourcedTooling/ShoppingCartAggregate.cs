using System.Threading;

namespace EventSourcedTooling
{
    public class ShoppingCartAggregate : ICommandHandler<StartShopping>, ICommandHandler<AddProductToCart>
    {
        public object Handle(StartShopping command)
        {
            return new CustomerStartedShopping(command.CustomerId, command.CartId);
        }

        public object Handle(AddProductToCart command)
        {
            throw new AbandonedMutexException();
        }
    }
}