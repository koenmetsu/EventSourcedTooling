using System;
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
        static async Task Main(string[] args)
        {
            var inMemoryStreamStore = new InMemoryStreamStore(() => DateTime.UtcNow);

            var streamId = Guid.NewGuid().ToString();
            var expectedVersion = ExpectedVersion.Any;

            var @event = new { Hello = "World"};
            var serializeObject = SimpleJson.SerializeObject(@event);
            var messageId = Guid.NewGuid();
            var mytype = "MyType";
            var newStreamMessage = 
                new NewStreamMessage(
                    messageId, 
                    mytype, 
                    serializeObject);

            await inMemoryStreamStore.AppendToStream(
                streamId, 
                expectedVersion,
                newStreamMessage);

            var readAllForwards = await inMemoryStreamStore.ReadAllForwards(Position.Start, Int32.MaxValue, true, CancellationToken.None);

            readAllForwards.Messages.ToList().ForEach(async message => Console.WriteLine($"{message.Type}: {await message.GetJsonData()}"));

            Console.ReadLine();
        }
    }
}
