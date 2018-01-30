using Xunit;

namespace EventSourcedTooling.Tests
{
    public class StartShoppingTest
    {
        [Fact]
        public void CustomerStartedShoppingWhenStartShopping()
        {
            new EventAsserter<StartShopping>(new CustomerStartedShoppingHandler())
                .When(new StartShopping("customerId", "cartId", "startTime"))
                .Then(new CustomerStartedShopping("customerId", "cartId"));
        }

        [Fact]
        public void CustomerStartedShoppingWhenStartBlahShopping()
        {
            new EventAsserter<StartShopping>(new CustomerStartedShoppingHandler())
                .Given(new CustomerStartedShopping("customerId", "cartId"))
                .When(new StartShopping("customerId", "cartId", "startTime"))
                .Then(new ProductWasAddedToCart());
        }
    }

}