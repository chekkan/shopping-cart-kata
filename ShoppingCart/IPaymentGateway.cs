namespace ShoppingCart
{
    public interface IPaymentGateway
    {
        PaymentReference Pay(Order order, UserId userId, PaymentDetails payment);
    }
}
