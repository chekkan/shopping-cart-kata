using System;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class BasketTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void BasketTotalAfterOneItem(int quantity)
        {
            var sut = new Basket(new UserId("john"), DateTime.Parse("2020-06-12"));
            sut.Add(new BasketItem(new ProductId(10001), quantity));
            Assert.Equal(quantity * 10m, sut.Total);
        }

        [Fact]
        public void TotalWithMultipleProducts()
        {
            var sut = new Basket(new UserId("john"), DateTime.Parse("2020-06-12"));
            sut.Add(new BasketItem(new ProductId(10001), 1));
            sut.Add(new BasketItem(new ProductId(10002), 1));
            Assert.Equal(10m + 5m, sut.Total);
        }
    }
}