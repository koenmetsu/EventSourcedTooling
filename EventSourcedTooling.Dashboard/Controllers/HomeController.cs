using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventSourcedTooling.Dashboard.Models;
using SqlStreamStore;
using SqlStreamStore.Streams;
using StreamStoreStore.Json;

namespace EventSourcedTooling.Dashboard.Controllers
{
    public class Message
    {
        public string Type { get; set; }
        public string Body { get; set; }
    }

    public class HomeController : Controller
    {
        private InMemoryStreamStore inMemoryStreamStore;

        public async Task<IActionResult> Index()
        {
            inMemoryStreamStore = new InMemoryStreamStore(() => DateTime.UtcNow);

            var customerId = "CUST-58";
            var cartId = "CART-2459";
            await NewMethod(new CustomerStartedShopping(customerId, cartId), customerId);
            await NewMethod(new ProductWasAddedToCart(customerId, cartId, "SKU-876", "56", "now"), customerId);
            await NewMethod(new ProductWasAddedToCart(customerId, cartId, "SKU-3", "9", "now"), customerId);
            await NewMethod(new CustomerPlacedOrder(customerId, cartId, new List<Product>(), "now"), customerId);

            var readAllForwards = await inMemoryStreamStore.ReadAllForwards(Position.Start, Int32.MaxValue, true, CancellationToken.None);

            // readAllForwards.Messages.ToList().ForEach(async message => Console.WriteLine($"{message.Type}: {await message.GetJsonData()}"));
            // readAllForwards.Messages.ToList().Select(async message => message);

            var enumerable = readAllForwards.Messages.Select(x => new Message {Body = x.GetJsonData().Result, Type = x.Type});
            ViewData["Messages"] = enumerable;

            return View(enumerable);
        }
        
        private async Task NewMethod(IEvent @event, string streamId)
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


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}