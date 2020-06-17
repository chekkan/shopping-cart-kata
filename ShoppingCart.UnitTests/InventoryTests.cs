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
    }
}
