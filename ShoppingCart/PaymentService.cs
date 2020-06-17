using System;
using System.Linq;

namespace ShoppingCart
{
    public class PaymentService
    {
        private readonly IOrderService orderService;
        private readonly IPaymentGateway paymentGateway;
        private readonly Inventory inventory;

        public PaymentService(IOrderService orderService,
                              IPaymentGateway paymentGateway,
                              Inventory inventory)
        {
            this.orderService = orderService;
            this.paymentGateway = paymentGateway;
            this.inventory = inventory;
        }

        public void MakePayment(UserId userId,
                                ShoppingCartId cartId,
                                PaymentDetails payment)
        {
            var order = this.orderService.Create(userId, cartId);
            try
            {
                this.paymentGateway.Pay(order, userId, payment);
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
