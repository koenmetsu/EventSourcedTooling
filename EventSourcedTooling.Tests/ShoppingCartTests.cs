using Xunit;

namespace EventSourcedTooling.Tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void CustomerStartedShoppingWhenStartShopping()
        {
            new AggregateAsserter<StartShopping>(new ShoppingCartAggregate())
                .When(new StartShopping("customerId", "cartId", "startTime"))
                .Then(new CustomerStartedShopping("customerId", "cartId"));
        }

        [Fact]
        public void CustomerStartedShoppingWhenStartBlahShopping()
        {
            new AggregateAsserter<AddProductToCart>(new ShoppingCartAggregate())
                .Given(new CustomerStartedShopping("customerId", "cartId"))
                .When(new AddProductToCart("cartId", "SKU", "price", "addTime"))
                .Then(new ProductWasAddedToCart("customerId", "cartId", "SKU", "price", "addTime"));
        }
    }

}