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
            var inventory = new Inventory();
            ProductId lor = new ProductId(1001);
            inventory.Add(lor, 20);
            var sut = new StockController(inventory);
            Assert.True(sut.CheckAvailability(lor, quantity));
        }

        [Theory]
        [InlineData(11)]
        [InlineData(19)]
        public void CheckAvailability_ReturnsFalse(int quantity)
        {
            var inventory = new Inventory();
            var hobbit = new ProductId(1002);
            inventory.Add(hobbit, 10);
            var sut = new StockController(inventory);
            Assert.False(sut.CheckAvailability(hobbit, quantity));
        }

        [Fact]
        public void CheckAvailability_WithMultipleProducts()
        {
            var inventory = new Inventory();
            var hobbit = new ProductId(1002);
            var lor = new ProductId(1001);
            inventory.Add(hobbit, 10);
            inventory.Add(lor, 20);
            var sut = new StockController(inventory);
            Assert.False(sut.CheckAvailability(hobbit, 11));
        }

        [Fact]
        public void CheckAvailability_ReturnsFalseWhenProductIsNotInitiallyAdded()
        {
            var inventory = new Inventory();
            var sut = new StockController(inventory);
            Assert.False(sut.CheckAvailability(new ProductId(1001), 1));
        }

        [Theory]
        [InlineData(20)]
        [InlineData(42)]
        public void CheckAvailability_AfterReserving(int quantity)
        {
            var inventory = new Inventory();
            ProductId lor = new ProductId(1002);
            inventory.Add(lor, quantity);
            var sut = new StockController(inventory);
            sut.Reserve(lor, quantity);
            Assert.False(sut.CheckAvailability(lor, 1));
        }

        [Fact]
        public void PriceForDvds()
        {
            var inventory = new Inventory();
            inventory.Add(new ProductId(20001), 1);

            var sut = new StockController(inventory);
            var actual = sut.PriceFor(new ProductId(20001));
            Assert.Equal(9m, actual);
        }
    }
}
