using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class PaymentServiceTests
    {
        private readonly UserId john;
        private readonly ShoppingCartId cartId;
        private readonly PaymentDetails payment;
        private readonly Mock<IPaymentGateway> paymentGatewayMock;
        private readonly Mock<IOrderService> orderSvcMock;

        public PaymentServiceTests()
        {
            this.john = new UserId("john");
            this.cartId = new ShoppingCartId(Guid.NewGuid().ToString());
            this.payment = new PaymentDetails();
            this.orderSvcMock = new Mock<IOrderService>();
            this.paymentGatewayMock = new Mock<IPaymentGateway>();
        }

        [Fact]
        public void CallsPaymentGatewayWithOrder()
        {
            var order = new Order(new OrderId(Guid.NewGuid().ToString()));
            orderSvcMock.Setup(svc => svc.Create(john, cartId))
                        .Returns(order);

            var sut = new PaymentService(
                orderSvcMock.Object,
                paymentGatewayMock.Object);
            sut.MakePayment(john, cartId, payment);

            paymentGatewayMock.Verify(gateway => gateway.Pay(order, john, payment));
        }

        [Fact]
        public void ThrowPaymentFailureException()
        {
             paymentGatewayMock.Setup(gw => gw.Pay(It.IsAny<Order>(), john, payment))
                .Throws(new Exception());

            var sut = new PaymentService(
                orderSvcMock.Object,
                paymentGatewayMock.Object);
            Assert.Throws<PaymentFailure>(
                () => sut.MakePayment(john, cartId, payment));
        }
    }
}
