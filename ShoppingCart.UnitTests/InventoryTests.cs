using System.IO;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class InventoryTests
    {
        [Theory]
        [InlineData(10001, 10)]
        [InlineData(10002, 8)]
        public void PrintInventoryWithOneItem(int pId, int quantity)
        {
            var expected = $"{quantity} x ProductId({pId})\n";
            var sw = new StringWriter();

            var sut = new Inventory();
            sut.Add(new ProductId(pId), quantity);
            sut.Print(sw);

            Assert.Equal(expected, sw.GetStringBuilder().ToString());
        }

        [Fact]
        public void PrintInventoryWithMultipleProducts()
        {
            var expected = "8 x ProductId(10001)\n12 x ProductId(10002)\n";
            var sw = new StringWriter();

            var sut = new Inventory();
            sut.Add(new ProductId(10001), 8);
            sut.Add(new ProductId(10002), 12);
            sut.Print(sw);

            Assert.Equal(expected, sw.GetStringBuilder().ToString());
        }
    }
}
