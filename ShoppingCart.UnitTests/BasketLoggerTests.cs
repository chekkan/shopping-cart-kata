using System;
using System.IO;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class BasketLoggerTests
    {
        [Theory]
        [InlineData("john", 10001, 4)]
        [InlineData("doe", 10002, 8)]
        public void LogsOnAddItem(string uId, int pId, int quantity)
        {
            var expected = $"[ITEM ADDED TO SHOPPING CART]: User[{uId}], Product[{pId}], Quantity[{quantity}]\n";
            var writer = new StringWriter();
            var sut = new BasketLogger(writer,
                                       new UserId(uId),
                                       DateTime.Parse("2020-02-12"),
                                       new Inventory());
            sut.Add(new BasketItem(new ProductId(pId), quantity));
            var sb = writer.GetStringBuilder();
            var actual = sb.ToString();
            Assert.Equal(expected, actual);
            Assert.Equal(1, sut.Items.Count);
        }
    }
}
