namespace ShoppingCart
{
    public class BasketItem
    {
        public ProductId ProductId { get; }
        public int Quantity { get; }

        public BasketItem(ProductId productId, int quantity)
        {
            this.ProductId = productId;
            this.Quantity = quantity;
        }
    }
}
