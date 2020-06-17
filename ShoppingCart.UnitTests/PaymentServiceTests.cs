using System;
using Moq;
using Xunit;

namespace ShoppingCart.UnitTests
{
    public class PaymentServiceTests
    {
        private static ProductId lordOfTheRings = new ProductId(10001);
        private static ProductId hobbit = new ProductId(10002);
        private static UserId john = new UserId("john");
        private static UserId ryan = new UserId("ryan");
        private readonly ShoppingCartId cartId;
        private readonly PaymentDetails payment;
        private readonly Mock<IPaymentGateway> paymentGatewayMock;
        private readonly Mock<IOrderService> orderSvcMock;
        private readonly Inventory inventory;
        private readonly PaymentService sut;

        public PaymentServiceTests()
        {
            this.cartId = new ShoppingCartId(Guid.NewGuid().ToString());
            this.payment = new PaymentDetails();
            this.orderSvcMock = new Mock<IOrderService>();
            this.paymentGatewayMock = new Mock<IPaymentGateway>();
            this.inventory = new Inventory();
            this.inventory.Add(lordOfTheRings, 4, 10m);
            this.inventory.Add(hobbit, 4, 5m);
            this.sut = new PaymentService(orderSvcMock.Object,
                                          paymentGatewayMock.Object,
                                          this.inventory);
        }

        [Fact]
        public void CallsPaymentGatewayWithOrder()
        {
            var basket = new Basket(john,
                                    DateTime.Parse("2020-12-01"),
                                    this.inventory);
            basket.Add(new BasketItem(lordOfTheRings, 2));
            var order = new Order(new OrderId(Guid.NewGuid().ToString()), basket);
            orderSvcMock.Setup(svc => svc.Create(john, cartId))
                        .Returns(order);

            this.sut.MakePayment(john, cartId, payment);

            paymentGatewayMock.Verify(gateway => gateway.Pay(order, john, payment));
        }

        [Fact]
        public void ThrowPaymentFailureException()
        {
             paymentGatewayMock.Setup(gw => gw.Pay(It.IsAny<Order>(), john, payment))
                .Throws(new Exception());

            Assert.Throws<PaymentFailure>(
                () => this.sut.MakePayment(john, cartId, payment));
        }

        [Fact]
        public void MarkReservedProductAsSold()
        {
            var basket = new Basket(ryan,
                                    DateTime.Parse("2020-12-01"),
                                    this.inventory);
            basket.Add(new BasketItem(lordOfTheRings, 2));
            var order = new Order(new OrderId(Guid.NewGuid().ToString()), basket);
            orderSvcMock.Setup(svc => svc.Create(ryan, cartId))
                .Returns(order);
            this.inventory.Reserve(lordOfTheRings, 4);

            this.sut.MakePayment(ryan, cartId, payment);
            Assert.Equal(2, this.inventory.AvailableQuantity(lordOfTheRings));
            Assert.Equal(4, this.inventory.AvailableQuantity(hobbit));
        }

        [Fact]
        public void MarkMultipleProductsAsSold()
        {
            var basket = new Basket(ryan,
                                    DateTime.Parse("2020-12-01"),
                                    this.inventory);
            basket.Add(new BasketItem(lordOfTheRings, 2));
            basket.Add(new BasketItem(hobbit, 3));
            var order = new Order(new OrderId(Guid.NewGuid().ToString()), basket);
            orderSvcMock.Setup(svc => svc.Create(ryan, cartId))
                .Returns(order);
            this.inventory.Reserve(lordOfTheRings, 4);

            this.sut.MakePayment(ryan, cartId, payment);
            Assert.Equal(2, this.inventory.AvailableQuantity(lordOfTheRings));
            Assert.Equal(1, this.inventory.AvailableQuantity(hobbit));
        }
    }
}
