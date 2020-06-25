using System;
using System.Linq;

namespace ShoppingCart
{
    public class PaymentService
    {
        private readonly IOrderService orderService;
        private readonly IPaymentGateway paymentGateway;
        private readonly Inventory inventory;
        private readonly IOrderConfirmation orderConfirmation;

        public PaymentService(IOrderService orderService,
                              IPaymentGateway paymentGateway,
                              Inventory inventory,
                              IOrderConfirmation orderConfirmation)
        {
            this.orderService = orderService;
            this.paymentGateway = paymentGateway;
            this.inventory = inventory;
            this.orderConfirmation = orderConfirmation;
        }

        public void MakePayment(UserId userId,
                                ShoppingCartId cartId,
                                PaymentDetails payment)
        {
            var order = this.orderService.Create(userId, cartId);
            try
            {
                var paymentReference = this.paymentGateway.Pay(order, userId, payment);
                this.orderConfirmation.Send(userId, order.Id, paymentReference);
                foreach (var item in order.Items)
                {
                    this.inventory.Sold(item.ProductId, item.Quantity);
                }
            }
            catch (Exception)
            {
                throw new PaymentFailure();
            }
        }
    }
}
