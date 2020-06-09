using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class ShoppingBasketServiceTests
    {
        [Fact]
        public void AddingItemCreatesBasket()
        {
            UserId john = new UserId("john");
            ProductId lor = new ProductId(10001);
            const int quantity = 1;
            var basketItem = new BasketItem(lor, quantity);
            var expected = new Basket(john);
            expected.Add(basketItem);

            var repoMock = new Mock<IBasketRepository>();
            repoMock.Setup((repo) => repo.GetBasket(john))
                .Returns(() => null);
            
            var sut = new ShoppingBasketService(repoMock.Object);
            sut.AddItem(john, lor, quantity);
            repoMock.Verify((repo) => repo.Save(expected));
        }

        [Fact]
        public void AddingItemToExistingBasket()
        {
            UserId ryan = new UserId("ryan");
            ProductId lor = new ProductId(10001);
            ProductId hobbit = new ProductId(10002);
            const int quantity = 5;
            var expected = new Basket(ryan);
            expected.Add(new BasketItem(lor, 2));
            expected.Add(new BasketItem(hobbit, quantity));
            var repoMock = new Mock<IBasketRepository>();
            Basket existingBasket = new Basket(ryan);
            existingBasket.Add(new BasketItem(lor, 2));
            repoMock.Setup(repo => repo.GetBasket(ryan)).Returns(existingBasket);

            var sut = new ShoppingBasketService(repoMock.Object);
            sut.AddItem(ryan, hobbit, quantity);
            
            repoMock.Verify(repo => repo.Save(expected));
        }
    }
}
