using System;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class BasketFactoryTests
    {
        [Fact]
        public void ImplementsIBasketFactory()
        {
            var clock = new ManualClock(DateTime.Parse("2020-05-10"));
            var sut = new BasketFactory(clock);
            Assert.IsAssignableFrom<IBasketFactory>(sut);
        }

        [Theory]
        [InlineData("2020-05-10")]
        [InlineData("2019-11-23")]
        public void CreateReturnsWithCurrentDate(string aDate)
        {
            var clock = new ManualClock(DateTime.Parse(aDate));
            var sut = new BasketFactory(clock);
            UserId userId = new UserId("john");
            var actual = sut.Create(userId);
            Assert.Equal(DateTime.Parse(aDate), actual.CreationDate);
            Assert.Same(userId, actual.UserId);
        }
    }
}