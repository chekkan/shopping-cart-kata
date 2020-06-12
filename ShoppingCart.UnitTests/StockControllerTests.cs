using Xunit;

namespace ShoppingCart.UnitTests
{
    public class StockControllerTests
    {
        [Theory]
        [InlineData(19)]
        [InlineData(20)]
        public void CheckAvailabilty_ReturnsTrue(int quantity)
        {
            var sut = new StockController();
            ProductId lor = new ProductId(1001);
            sut.AddProduct(lor, 20);
            Assert.True(sut.CheckAvailability(lor, quantity));
        }

        [Theory]
        [InlineData(11)]
        [InlineData(19)]
        public void CheckAvailability_ReturnsFalse(int quantity)
        {
            var sut = new StockController();
            var hobbit = new ProductId(1002);
            sut.AddProduct(hobbit, 10);
            Assert.False(sut.CheckAvailability(hobbit, quantity));
        }

        [Fact]
        public void CheckAvailability_WithMultipleProducts()
        {
            var sut = new StockController();
            var hobbit = new ProductId(1002);
            var lor = new ProductId(1001);
            sut.AddProduct(hobbit, 10);
            sut.AddProduct(lor, 20);
            Assert.False(sut.CheckAvailability(hobbit, 11));
        }

        [Fact]
        public void CheckAvailability_ReturnsFalseWhenProductIsNotInitiallyAdded()
        {
            var sut = new StockController();
            Assert.False(sut.CheckAvailability(new ProductId(1001), 1));
        }

        [Theory]
        [InlineData(20)]
        [InlineData(42)]
        public void CheckAvailability_AfterReserving(int quantity)
        {
            ProductId lor = new ProductId(1002);
            var sut = new StockController();
            sut.AddProduct(lor, quantity);
            sut.Reserve(lor, quantity);
            Assert.False(sut.CheckAvailability(lor, 1));
        }

        [Fact]
        public void PriceForDvds()
        {
            var sut = new StockController();
            sut.AddProduct(new ProductId(20001), 1);
            var actual = sut.PriceFor(new ProductId(20001));
            Assert.Equal(9m, actual);
        }
    }
}
