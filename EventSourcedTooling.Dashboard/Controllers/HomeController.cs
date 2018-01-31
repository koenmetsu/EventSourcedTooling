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

        public HomeController()
        {
            inMemoryStreamStore = new InMemoryStreamStore(() => DateTime.UtcNow);

            var customer1 = "CUST-58";
            var cartId = "CART-2459";
            NewMethod(new CustomerStartedShopping(customer1, cartId), customer1);
            NewMethod(new ProductWasAddedToCart(customer1, cartId, "SKU-876", "56", "now"), customer1);
            NewMethod(new ProductWasAddedToCart(customer1, cartId, "SKU-3", "9", "now"), customer1);
            NewMethod(new CustomerPlacedOrder(customer1, cartId, new List<Product>(), "now"), customer1);

            var customer2 = "CUST-34";
            NewMethod(new CustomerStartedShopping(customer2, cartId), customer2);
            NewMethod(new ProductWasAddedToCart(customer2, cartId, "SKU-876", "56", "now"), customer2);
            NewMethod(new ProductWasAddedToCart(customer2, cartId, "SKU-3", "9", "now"), customer2);
            NewMethod(new CustomerPlacedOrder(customer2, cartId, new List<Product>(), "now"), customer2);
        }

        public async Task<IActionResult> Index(string streamId)
        {
            StreamMessage[] messages;
            if (string.IsNullOrEmpty(streamId))
            {
                messages = inMemoryStreamStore.ReadAllForwards(Position.Start, Int32.MaxValue, true, CancellationToken.None).Result.Messages;
            }
            else
            {
                messages = inMemoryStreamStore.ReadStreamForwards(new StreamId(streamId), 0, int.MaxValue).Result.Messages;
                
            }
            
            var enumerable = messages.Select(x => new Message {Body = x.GetJsonData().Result, Type = x.Type});

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