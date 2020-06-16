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
            try
            {
                this.paymentGateway.Pay(order, userId, payment);
            }
            catch (Exception e)
            {
                throw new PaymentFailure();
            }
        }
    }
}
