using System;
using System.Collections.Generic;

namespace ShoppingCart
{
    public class Basket
    {
        private List<BasketItem> items;
        private readonly Inventory inventory;

        public Basket(UserId userId, DateTime creationDate, Inventory inventory)
        {
            this.items = new List<BasketItem>();
            this.inventory = inventory;
            UserId = userId;
            CreationDate = creationDate;
            Id = new ShoppingCartId(Guid.NewGuid().ToString());
        }

        public UserId UserId { get; }
        public DateTime CreationDate { get; set; }
        public ShoppingCartId Id { get; }

        public IReadOnlyCollection<BasketItem> Items => this.items;

        public decimal Total
            => new BasketCalculator(this.inventory).Calculate(this.Items);

        public virtual void Add(BasketItem item)
        {
            this.items.Add(item);
        }
    }
}
