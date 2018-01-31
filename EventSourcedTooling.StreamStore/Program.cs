using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SqlStreamStore;
using SqlStreamStore.Streams;
using StreamStoreStore.Json;

namespace EventSourcedTooling.StreamStore
{
    class Program
    {
        private static InMemoryStreamStore inMemoryStreamStore;

        static async Task Main(string[] args)
        {
            inMemoryStreamStore = new InMemoryStreamStore(() => DateTime.UtcNow);

            var customerId = "CUST-58";
            var cartId = "CART-2459";
            await NewMethod(new CustomerStartedShopping(customerId, cartId), customerId);
            await NewMethod(new ProductWasAddedToCart(customerId, cartId, "SKU-876", "56", "now"), customerId);
            await NewMethod(new ProductWasAddedToCart(customerId, cartId, "SKU-3", "9", "now"), customerId);
            await NewMethod(new CustomerPlacedOrder(customerId, cartId, new List<Product>(), "now"), customerId);

            var readAllForwards = await inMemoryStreamStore.ReadAllForwards(Position.Start, Int32.MaxValue, true, CancellationToken.None);

            readAllForwards.Messages.ToList().ForEach(async message => Console.WriteLine($"{message.Type}: {await message.GetJsonData()}"));
        }

        private static async Task NewMethod(IEvent @event, string streamId)
        {
            var newStreamMessage =
                new NewStreamMessage(
                    Guid.NewGuid(),
                    @event.GetType().Name,
                    SimpleJson.SerializeObject(@event));

            await inMemoryStreamStore.AppendToStream(
                streamId,
                ExpectedVersion.Any,
                newStreamMessage);
        }
    }
}
