using System;
using System.IO;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class BasketFactoryTests
    {
        private readonly TextWriter writer;
        private readonly Inventory inventory;

        public BasketFactoryTests()
        {
            this.writer = new StringWriter();
            var purchaseSystemStub = new Mock<IPurchaseSystem>().Object;
            this.inventory = new Inventory(purchaseSystemStub);
        }

        [Fact]
        public void ImplementsIBasketFactory()
        {
            var clock = new ManualClock(DateTime.Parse("2020-05-10"));
            var sut = new BasketFactory(clock, this.writer, this.inventory);
            Assert.IsAssignableFrom<IBasketFactory>(sut);
        }

        [Theory]
        [InlineData("2020-05-10")]
        [InlineData("2019-11-23")]
        public void CreateReturnsWithCurrentDate(string aDate)
        {
            var clock = new ManualClock(DateTime.Parse(aDate));
            var sut = new BasketFactory(clock, this.writer, this.inventory);
            UserId userId = new UserId("john");
            var actual = sut.Create(userId);
            Assert.Equal(DateTime.Parse(aDate), actual.CreationDate);
            Assert.Same(userId, actual.UserId);
        }

        [Fact]
        public void CreateReturnsBasketLogger()
        {
            var clock = new ManualClock(DateTime.Parse("2019-09-21"));
            var sut = new BasketFactory(clock, this.writer, this.inventory);
            var actual = sut.Create(new UserId("john"));
            Assert.IsType<BasketLogger>(actual);
        }
    }
}
