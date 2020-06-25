using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class BasketTests
    {
        private static ProductId lordOfTheRings = new ProductId(10001);
        private static ProductId hobbit = new ProductId(10002);
        private static ProductId gameOfThrones = new ProductId(20001);
        private static ProductId breakingBad = new ProductId(20110);
        private Basket sut;

        public BasketTests()
        {
            var purchaseSystemStub = new Mock<IPurchaseSystem>().Object;
            var inventory = new Inventory(purchaseSystemStub);
            inventory.Add(lordOfTheRings, 10, 10m);
            inventory.Add(hobbit, 12, 5m);
            inventory.Add(gameOfThrones, 5, 9m);
            inventory.Add(breakingBad, 2, 7m);
            sut = new Basket(new UserId("john"),
                             DateTime.Parse("2020-06-12"),
                             inventory);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void BasketTotalAfterOneItem(int quantity)
        {
            sut.Add(new BasketItem(lordOfTheRings, quantity));
            Assert.Equal(quantity * 10m, sut.Total);
        }

        [Fact]
        public void TotalWithMultipleProducts()
        {
            sut.Add(new BasketItem(lordOfTheRings, 1));
            sut.Add(new BasketItem(hobbit, 1));
            Assert.Equal(10m + 5m, sut.Total);
        }

        [Fact]
        public void MultiBuyDiscount_MoreThanThreeBooks()
        {
            sut.Add(new BasketItem(lordOfTheRings, 1));
            sut.Add(new BasketItem(hobbit, 3));
            Assert.Equal(.9m * (10m + 5m * 3), sut.Total);
        }

        [Theory]
        [InlineData(20001, 9.0)]
        [InlineData(20110, 7.0)]
        public void MultiBuyDiscount_ForOneBookAndDvd(int pId, decimal price)
        {
            sut.Add(new BasketItem(lordOfTheRings, 1));
            sut.Add(new BasketItem(new ProductId(pId), 1));
            Assert.Equal((10m + price) * .8m, sut.Total);
        }

        [Fact]
        public void MultiBuyDiscount_BothApplyChooseBiggest()
        {
            sut.Add(new BasketItem(lordOfTheRings, 4));
            sut.Add(new BasketItem(gameOfThrones, 1));
            Assert.Equal((10m * 4 + 9m) * .8m, sut.Total);
        }
    }
}
