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
    }

}