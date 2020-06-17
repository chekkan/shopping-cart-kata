using System;
using System.Collections.Generic;

namespace ShoppingCart
{
    public class Basket
    {
        private List<BasketItem> items;
        public IReadOnlyCollection<BasketItem> Items => this.items;
        public Basket(UserId userId, DateTime creationDate)
        {
            items = new List<BasketItem>();
            UserId = userId;
            CreationDate = creationDate;
            Id = new ShoppingCartId(Guid.NewGuid().ToString());
        }

        public UserId UserId { get; }
        public DateTime CreationDate { get; set; }
        public ShoppingCartId Id { get; }

        public decimal Total
        {
            get
            {
                return new BasketCalculator(new StockController(new Inventory()))
                    .Calculate(this.Items);
            }
        }

        public virtual void Add(BasketItem item)
        {
            this.items.Add(item);
        }
    }
}
