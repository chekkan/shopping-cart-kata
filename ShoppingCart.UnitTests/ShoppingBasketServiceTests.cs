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
                                                basketFactoryMock.Object);
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
                                                basketFactoryMock.Object);
            sut.AddItem(ryan, hobbit, quantity);
            
            repoMock.Verify(repo => repo.Save(existingBasket));
            Assert.Equal(1, existingBasket.Items.Count);
        }
    }
}
