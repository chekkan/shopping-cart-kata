namespace ShoppingCart
{
    public interface IOrderService
    {
        Order Create(UserId userId, ShoppingCartId cartId);
    }
}
