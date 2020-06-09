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

        public override bool Equals(object obj)
        {
            var item = obj as BasketItem;
            if(item == null)
            {
                return false;
            }
            return item.productId == productId && item.quantity == quantity;
        }

        public override int GetHashCode() 
            => productId.GetHashCode() ^ quantity.GetHashCode();
    }
}
