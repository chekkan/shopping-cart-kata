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

        private readonly Inventory inventory;
        private readonly Basket johnsBasket;
        private readonly Basket ryansBasket;
        private readonly Mock<IBasketRepository> repoMock;
        private readonly Mock<IBasketFactory> basketFactoryMock;
        private readonly ShoppingBasketService sut;

        public ShoppingBasketServiceTests()
        {
            this.inventory = new Inventory();
            this.inventory.Add(lor, 5, 10m);
            this.inventory.Add(hobbit, 5, 5m);

            this.johnsBasket = new Basket(john,
                                          DateTime.Parse("2020-05-11"),
                                          this.inventory);
            this.ryansBasket = new Basket(ryan,
                                          DateTime.Parse("2012-02-12"),
                                          this.inventory);

            this.repoMock = new Mock<IBasketRepository>();
            this.basketFactoryMock = new Mock<IBasketFactory>();
            this.sut = new ShoppingBasketService(repoMock.Object,
                                                 basketFactoryMock.Object,
                                                 inventory);
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
            Assert.False(this.inventory.CheckAvailability(hobbit, 1));
        }

        [Fact]
        public void ReserveIsCalledOnlyAfterSave()
        {
            repoMock.Setup(repo => repo.Save(ryansBasket))
                .Throws(new Exception());
            basketFactoryMock.Setup(factory => factory.Create(ryan))
                .Returns(ryansBasket);

            Assert.Throws<Exception>(() => sut.AddItem(ryan, hobbit, 5));
            Assert.True(this.inventory.CheckAvailability(hobbit, 5));
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
