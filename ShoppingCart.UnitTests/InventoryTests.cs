using System.IO;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class InventoryTests
    {
        private static ProductId lordOfTheRings = new ProductId(10001);
        private static ProductId hobbit = new ProductId(10002);
        private static ProductId gameOfThrones = new ProductId(20001);

        private readonly Mock<IPurchaseSystem> purchaseSystemMock;
        private readonly Inventory sut;

        public InventoryTests()
        {
            this.purchaseSystemMock = new Mock<IPurchaseSystem>();
            this.sut = new Inventory(purchaseSystemMock.Object);
        }

        [Theory]
        [InlineData(9, 10001, 10)]
        [InlineData(11, 10002, 8)]
        public void PrintInventoryWithOneItem(int price, int pId, int quantity)
        {
            var pString = $"{price:c}".PadLeft(6, ' ');
            var expected = $"ProductId({pId}) | {pString} | {quantity}\n";
            var sw = new StringWriter();

            this.sut.Add(new ProductId(pId), quantity, price);
            this.sut.Print(sw);

            Assert.Equal(expected, sw.GetStringBuilder().ToString());
        }

        [Fact]
        public void PrintInventoryWithMultipleProducts()
        {
            var expected = "ProductId(10001) | £10.00 | 8\n" +
                "ProductId(10002) |  £5.00 | 12\n";
            var sw = new StringWriter();

            this.sut.Add(lordOfTheRings, 8, 10m);
            this.sut.Add(hobbit, 12, 5m);
            this.sut.Print(sw);

            Assert.Equal(expected, sw.GetStringBuilder().ToString());
        }

        [Fact]
        public void PriceForDvds()
        {
            this.sut.Add(gameOfThrones, 1, 9m);

            var actual = this.sut.PriceFor(gameOfThrones);
            Assert.Equal(9m, actual);
        }

        [Theory]
        [InlineData(200, 198, 2)]
        [InlineData(200, 197, 3)]
        [InlineData(20, 1, 19)]
        public void Sold_NotifiesPurchaseSystem(int stock, int quantity, int remaining)
        {
            this.sut.Add(lordOfTheRings, stock, 10m);
            this.sut.Sold(lordOfTheRings, quantity);
            this.purchaseSystemMock
                .Verify(ps => ps.OrderMore(lordOfTheRings, remaining));
        }

        [Fact]
        public void Sold_DoesntNotifyPurchaseSystemIfStockIsAboveThreshold()
        {
            this.sut.Add(lordOfTheRings, 200, 10m);
            this.sut.Sold(lordOfTheRings, 180);
            this.purchaseSystemMock.Verify(ps => ps.OrderMore(
                                                              It.IsAny<ProductId>(),
                                                              It.IsAny<int>()),
                                           Times.Never());
        }
    }
}
