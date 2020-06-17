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
        private static Basket johnsBasket = new Basket(john,
                                                       DateTime.Parse("2020-05-11"));
        private static Basket ryansBasket = new Basket(ryan,
                                                       DateTime.Parse("2012-02-12"));
        private readonly StockController stockController;
        private readonly Mock<IBasketRepository> repoMock;
        private readonly Mock<IBasketFactory> basketFactoryMock;
        private readonly ShoppingBasketService sut;

        public ShoppingBasketServiceTests()
        {
            var inventory = new Inventory();
            inventory.Add(lor, 5);
            inventory.Add(hobbit, 5);
            stockController = new StockController(inventory);
            repoMock = new Mock<IBasketRepository>();
            basketFactoryMock = new Mock<IBasketFactory>();
            sut = new ShoppingBasketService(repoMock.Object, basketFactoryMock.Object,
                                            stockController);
        }

        [Fact]
        public void AddingItemCreatesBasket()
        {
            repoMock.Setup((repo) => repo.GetBasket(john))
                .Returns(() => null);
            basketFactoryMock.Setup(factory => factory.Create(john))
                .Returns(johnsBasket);

            sut.AddItem(john, lor, 1);
            repoMock.Verify((repo) => repo.Save(johnsBasket));
            Assert.Equal(1, johnsBasket.Items.Count);
        }

        [Fact]
        public void AddingItemToExistingBasket()
        {
            const int quantity = 5;
            repoMock.Setup(repo => repo.GetBasket(ryan))
                .Returns(ryansBasket);

            sut.AddItem(ryan, hobbit, quantity);

            repoMock.Verify(repo => repo.Save(ryansBasket));
            Assert.Equal(1, ryansBasket.Items.Count);
        }

        [Fact]
        public void ThrowsExceptionWhenOutOfStock()
        {
            Assert.Throws<OutOfStockException>(() => sut.AddItem(ryan, hobbit, 21));
        }

        [Fact]
        public void ReservesProductAfterSuccessfullySaving()
        {
            basketFactoryMock.Setup(factory => factory.Create(ryan))
                .Returns(ryansBasket);

            sut.AddItem(ryan, hobbit, 5);
            Assert.False(stockController.CheckAvailability(hobbit, 1));
        }

        [Fact]
        public void ReserveIsCalledOnlyAfterSave()
        {
            repoMock.Setup(repo => repo.Save(ryansBasket))
                .Throws(new Exception());
            basketFactoryMock.Setup(factory => factory.Create(ryan))
                .Returns(ryansBasket);

            Assert.Throws<Exception>(() => sut.AddItem(ryan, hobbit, 5));
            Assert.True(stockController.CheckAvailability(hobbit, 5));
        }

        [Fact]
        public void BasketFor_ReturnsBasketFromRepository()
        {
            repoMock.Setup(repo => repo.GetBasket(ryan))
                .Returns(ryansBasket);

            var basket = sut.BasketFor(ryan);
            Assert.Equal(ryansBasket, basket);
        }
    }
}
