using Xunit;

namespace EventSourcedTooling.Tests
{
    public class StartShoppingTest
    {
        [Fact]
        public void CustomerStartedShoppingWhenStartShopping()
        {
            new EventAsserter<StartShopping>(new CustomerStartedShoppingHandler())
                .When(new StartShopping("", "", ""))
                .Then(new CustomerStartedShopping("", ""));
        }
    }

}