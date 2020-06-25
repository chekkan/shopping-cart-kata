namespace ShoppingCart
{
    public interface IPurchaseSystem
    {
        void OrderMore(ProductId productId, int actualQuantity);
    }
}
