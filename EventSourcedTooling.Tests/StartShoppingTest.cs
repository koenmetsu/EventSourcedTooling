using Xunit;

namespace EventSourcedTooling.Tests
{
    public class StartShoppingTest
    {
        [Fact]
        public void CustomerStartedShoppingWhenStartShopping()
        {
            When(new StartShopping("", "", "")).Then(new CustomerStartedShopping("", ""));
        }

        private IBuilder When(StartShopping startShopping)
        {
            return new TestBuilder(startShopping);
        }
    }

    internal class TestBuilder : IBuilder
    {
        private CustomerStartedShopping evt;

        public TestBuilder(StartShopping startShopping)
        {
            evt = new CommandHandler().Handle(startShopping);

        }

        public void Then(CustomerStartedShopping customerStartedShopping)
        {
            Assert.Equal(evt, customerStartedShopping);
        }
    }

    internal class CommandHandler
    {
        public CustomerStartedShopping Handle(StartShopping startShopping)
        {
            return new CustomerStartedShopping();
        }
    }

    internal interface IBuilder
    {
        void Then(CustomerStartedShopping customerStartedShopping);
    }
}