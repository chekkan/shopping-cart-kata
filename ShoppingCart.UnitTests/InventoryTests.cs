using System.IO;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class InventoryTests
    {
        [Theory]
        [InlineData(9, 10001, 10)]
        [InlineData(11, 10002, 8)]
        public void PrintInventoryWithOneItem(int price, int pId, int quantity)
        {
            var pString = $"{price:c}".PadLeft(6, ' ');
            var expected = $"ProductId({pId}) | {pString} | {quantity}\n";
            var sw = new StringWriter();

            var sut = new Inventory();
            sut.Add(new ProductId(pId), quantity, price);
            sut.Print(sw);

            Assert.Equal(expected, sw.GetStringBuilder().ToString());
        }

        [Fact]
        public void PrintInventoryWithMultipleProducts()
        {
            var expected = "ProductId(10001) | £10.00 | 8\n" +
                "ProductId(10002) |  £5.00 | 12\n";
            var sw = new StringWriter();

            var sut = new Inventory();
            sut.Add(new ProductId(10001), 8, 10m);
            sut.Add(new ProductId(10002), 12, 5m);
            sut.Print(sw);

            Assert.Equal(expected, sw.GetStringBuilder().ToString());
        }

        [Fact]
        public void PriceForDvds()
        {
            var sut = new Inventory();
            sut.Add(new ProductId(20001), 1, 9m);

            var actual = sut.PriceFor(new ProductId(20001));
            Assert.Equal(9m, actual);
        }
    }
}
