using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class PaymentServiceTests
    {
        [Fact]
        public void CallsPaymentGatewayWithOrder()
        {
            var john = new UserId("john");
            var cartId = new ShoppingCartId(Guid.NewGuid().ToString());
            var payment = new PaymentDetails();
            var order = new Order(new OrderId(Guid.NewGuid().ToString()));

            var paymentGatewayMock = new Mock<IPaymentGateway>();

            var orderSvcMock = new Mock<IOrderService>();
            orderSvcMock.Setup(svc => svc.Create(john, cartId))
                        .Returns(order);

            var sut = new PaymentService(orderSvcMock.Object, paymentGatewayMock.Object);
            sut.MakePayment(john, cartId, payment);

            paymentGatewayMock.Verify(gateway => gateway.Pay(order, john, payment));
        }
    }
}
