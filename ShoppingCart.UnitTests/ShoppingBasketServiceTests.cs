using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class ShoppingBasketServiceTests
    {
        private static UserId john = new UserId("john");
        private static UserId ryan = new UserId("ryan");
        private static ProductId lor = new ProductId(10001);
        private static ProductId hobbit = new ProductId(10002);
        private static DateTime creationDate = DateTime.Parse("2020-05-11");
        private readonly StockController stockController;

        public ShoppingBasketServiceTests()
        {
            var inventory = new Inventory();
            inventory.Add(lor, 5);
            inventory.Add(hobbit, 5);
            stockController = new StockController(inventory);
        }

        [Fact]
        public void AddingItemCreatesBasket()
        {
            const int quantity = 1;
            var basketItem = new BasketItem(lor, quantity);
            var expected = new Basket(john, creationDate);

            var repoMock = new Mock<IBasketRepository>();
            repoMock.Setup((repo) => repo.GetBasket(john))
                .Returns(() => null);

            var basketFactoryMock = new Mock<IBasketFactory>();
            basketFactoryMock.Setup(factory => factory.Create(john))
                .Returns(expected);

            var sut = new ShoppingBasketService(repoMock.Object,
                                                basketFactoryMock.Object,
                                                stockController);
            sut.AddItem(john, lor, quantity);
            repoMock.Verify((repo) => repo.Save(expected));
            Assert.Equal(1, expected.Items.Count);
        }

        [Fact]
        public void AddingItemToExistingBasket()
        {
            const int quantity = 5;
            var repoMock = new Mock<IBasketRepository>();
            Basket existingBasket = new Basket(ryan, creationDate);
            repoMock.Setup(repo => repo.GetBasket(ryan))
                .Returns(existingBasket);

            var basketFactoryMock = new Mock<IBasketFactory>();

            var sut = new ShoppingBasketService(repoMock.Object,
                                                basketFactoryMock.Object,
                                                stockController);
            sut.AddItem(ryan, hobbit, quantity);

            repoMock.Verify(repo => repo.Save(existingBasket));
            Assert.Equal(1, existingBasket.Items.Count);
        }

        [Fact]
        public void ThrowsExceptionWhenOutOfStock()
        {
            var repoMock = new Mock<IBasketRepository>();
            var basketFactoryMock = new Mock<IBasketFactory>();
            var inventory = new Inventory();
            inventory.Add(hobbit, 20);
            var stock = new StockController(inventory);
            var sut = new ShoppingBasketService(repoMock.Object,
                                                basketFactoryMock.Object,
                                                stock);

            Assert.Throws<OutOfStockException>(() => sut.AddItem(ryan, hobbit, 21));
        }

        [Fact]
        public void ReservesProductAfterSuccessfullySaving()
        {
            var repoMock = new Mock<IBasketRepository>();
            var basketFactoryMock = new Mock<IBasketFactory>();
            basketFactoryMock.Setup(factory => factory.Create(ryan))
                .Returns(new Basket(ryan, DateTime.Parse("2012-02-12")));
            var sut = new ShoppingBasketService(repoMock.Object,
                                                basketFactoryMock.Object,
                                                stockController);
            sut.AddItem(ryan, hobbit, 5);
            Assert.False(stockController.CheckAvailability(hobbit, 1));
        }

        [Fact]
        public void ReserveIsCalledOnlyAfterSave()
        {
            Basket basket = new Basket(ryan, DateTime.Parse("2012-02-12"));
            var repoMock = new Mock<IBasketRepository>();
            repoMock.Setup(repo => repo.Save(basket)).Throws(new Exception());
            var basketFactoryMock = new Mock<IBasketFactory>();
            basketFactoryMock.Setup(factory => factory.Create(ryan))
                .Returns(basket);
            var sut = new ShoppingBasketService(repoMock.Object,
                                                basketFactoryMock.Object,
                                                stockController);
            Assert.Throws<Exception>(() => sut.AddItem(ryan, hobbit, 5));
            Assert.True(stockController.CheckAvailability(hobbit, 5));
        }
    }
}
