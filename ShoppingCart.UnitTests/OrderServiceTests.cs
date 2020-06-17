using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class OrderServiceTests
    {
        [Fact]
        public void ImplementsIOrderService()
        {
            var orderIdGenMock = new Mock<IOrderIdGenerator>();
            var basketRepoMock = new Mock<IBasketRepository>();
            var sut = new OrderService(orderIdGenMock.Object, basketRepoMock.Object);
            Assert.IsAssignableFrom<IOrderService>(sut);
        }

        [Fact]
        public void CreateReturnsNewOrder()
        {
            var john = new UserId("john");
            var cartId = new ShoppingCartId(Guid.NewGuid().ToString());
            var orderId = new OrderId(Guid.NewGuid().ToString());

            var orderIdGenMock = new Mock<IOrderIdGenerator>();
            var basketRepoMock = new Mock<IBasketRepository>();
            orderIdGenMock.Setup(gen => gen.Next()).Returns(orderId);

            var sut = new OrderService(orderIdGenMock.Object, basketRepoMock.Object);
            var actual = sut.Create(john, cartId);

            Assert.Equal(orderId, actual.Id);
        }
    }
}
