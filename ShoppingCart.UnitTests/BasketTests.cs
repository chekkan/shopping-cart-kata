using System;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class BasketTests
    {
        private Basket sut;

        public BasketTests()
        {
            sut = new Basket(new UserId("john"), DateTime.Parse("2020-06-12"));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void BasketTotalAfterOneItem(int quantity)
        {
            sut.Add(new BasketItem(new ProductId(10001), quantity));
            Assert.Equal(quantity * 10m, sut.Total);
        }

        [Fact]
        public void TotalWithMultipleProducts()
        {
            sut.Add(new BasketItem(new ProductId(10001), 1));
            sut.Add(new BasketItem(new ProductId(10002), 1));
            Assert.Equal(10m + 5m, sut.Total);
        }

        [Fact]
        public void MultiBuyDiscount_MoreThanThreeBooks()
        {
            sut.Add(new BasketItem(new ProductId(10001), 1));
            sut.Add(new BasketItem(new ProductId(10002), 3));
            Assert.Equal(.9m * (10m + 5m * 3), sut.Total);
        }

        [Theory]
        [InlineData(20001, 9.0)]
        [InlineData(20110, 7.0)]
        public void MultiBuyDiscount_ForOneBookAndDvd(int pId, decimal price)
        {
            sut.Add(new BasketItem(new ProductId(10001), 1));
            sut.Add(new BasketItem(new ProductId(pId), 1));
            Assert.Equal((10m + price) * .8m, sut.Total);
        }
    }
}