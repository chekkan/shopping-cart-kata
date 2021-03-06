using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class InMemoryBasketRepositoryTests
    {
        private static DateTime creationDate = DateTime.Parse("2020-05-08");
        private readonly Inventory inventory;

        public InMemoryBasketRepositoryTests()
        {
            var purchaseSystemStub = new Mock<IPurchaseSystem>().Object;
            this.inventory = new Inventory(purchaseSystemStub);
        }

        [Fact]
        public void ImplementsIBasketRepository()
        {
            var sut = new InMemoryBasketRepository();
            Assert.IsAssignableFrom<IBasketRepository>(sut);
        }

        [Fact]
        public void CanSaveAndRetrieveBasket()
        {
            UserId userId = new UserId("foo");
            var sut = new InMemoryBasketRepository();
            Basket basket = new Basket(userId, creationDate, this.inventory);
            sut.Save(basket);
            var actual = sut.GetBasket(userId);
            Assert.Same(basket, actual);
        }

        [Fact]
        public void CanStoreMultipleUserBaskets()
        {
            UserId userId = new UserId("foo");
            var sut = new InMemoryBasketRepository();
            Basket basket = new Basket(userId, creationDate, this.inventory);
            sut.Save(basket);
            sut.Save(new Basket(new UserId("bar"), creationDate, this.inventory));
            var actual = sut.GetBasket(userId);
            Assert.Same(basket, actual);
        }

        [Fact]
        public void Save_SameUsersBasketOverwrites()
        {
            var sut = new InMemoryBasketRepository();
            UserId john = new UserId("john");
            sut.Save(new Basket(john, creationDate, this.inventory));
            Basket basket = new Basket(john, creationDate, this.inventory);
            basket.Add(new BasketItem(new ProductId(20001), 5));
            sut.Save(basket);
            var actual = sut.GetBasket(john);
            Assert.Same(basket, actual);
        }

        [Fact]
        public void GetBasket_ReturnsNullWhenBasketDoesntExistsForUser()
        {
            var sut = new InMemoryBasketRepository();
            var basket = sut.GetBasket(new UserId("ryan"));
            Assert.Null(basket);
        }
    }
}
