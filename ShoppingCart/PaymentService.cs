using System;

namespace ShoppingCart
{
    public class PaymentService
    {
        private readonly IOrderService orderService;
        private readonly IPaymentGateway paymentGateway;

        public PaymentService(IOrderService orderService,
                              IPaymentGateway paymentGateway)
        {
            this.orderService = orderService;
            this.paymentGateway = paymentGateway;
        }

        public void MakePayment(UserId userId,
                                ShoppingCartId cartId,
                                PaymentDetails payment)
        {
            var order = this.orderService.Create(userId, cartId);
            this.paymentGateway.Pay(order, userId, payment);
        }
    }
}
