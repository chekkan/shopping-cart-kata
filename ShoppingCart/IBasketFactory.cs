#nullable enable

namespace ShoppingCart
{
    public interface IBasketFactory
    {
        Basket Create(UserId userId);
    }
}
