using System.Collections.Generic;

namespace ShoppingCart
{
    public class Order
    {
        private readonly OrderId id;
        private readonly Basket basket;

        public Order(OrderId id, Basket basket)
        {
            this.id = id;
            this.basket = basket;
        }

        public OrderId Id => id;
        public IReadOnlyCollection<BasketItem> Items => this.basket.Items;
    }
}
