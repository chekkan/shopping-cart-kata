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
        }

        public UserId UserId { get; }
        public DateTime CreationDate { get; set; }

        public void Add(BasketItem item)
        {
            this.items.Add(item);
        }
    }
}
