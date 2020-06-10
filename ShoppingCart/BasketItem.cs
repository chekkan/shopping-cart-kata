namespace ShoppingCart
{
    public class BasketItem
    {
        private readonly ProductId productId;
        private readonly int quantity;

        public BasketItem(ProductId productId, int quantity)
        {
            this.productId = productId;
            this.quantity = quantity;
        }
    }
}
