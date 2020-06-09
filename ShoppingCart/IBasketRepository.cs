#nullable enable

namespace ShoppingCart
{
    public interface IBasketRepository
    {
        void Save(Basket expected);
        Basket? GetBasket(UserId john);
    }
}
