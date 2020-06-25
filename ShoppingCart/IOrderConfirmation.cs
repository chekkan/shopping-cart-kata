namespace ShoppingCart
{
    public interface IOrderConfirmation
    {
        void Send(UserId userId, OrderId orderId, PaymentReference paymentReference);
    }
}
