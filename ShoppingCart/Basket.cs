using System;
using System.Collections.Generic;
using System.Linq;

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
        public decimal Total
        {
            get
            {
                return new BasketCalculator(new StockController())
                    .Calculate(this.Items);
            }
        }

        public void Add(BasketItem item)
        {
            this.items.Add(item);
        }
    }

    public class BasketCalculator
    {
        private readonly StockController stock;

        public BasketCalculator(StockController stock)
        {
            this.stock = stock;
        }

        public decimal Calculate(IReadOnlyCollection<BasketItem> items) 
            => items.Select(item => item.Quantity * this.stock.PriceFor(item.ProductId))
                    .Sum();
    }
}
