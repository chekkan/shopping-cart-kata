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
                return new BasketCalculator(new StockController(new Inventory()))
                    .Calculate(this.Items);
            }
        }

        public virtual void Add(BasketItem item)
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
        {
            var total = items.Select(item => item.Quantity * this.stock.PriceFor(item.ProductId))
                .Sum();
            int noOfBooks = items.Where(item => item.ProductId.Type == ProductTypes.Book)
                .Sum(item => item.Quantity);
            int noOfDvds = items.Where(item => item.ProductId.Type == ProductTypes.Dvd)
                .Sum(i => i.Quantity);
            if (noOfBooks >= 1 && noOfDvds >= 1) {
                return total * .8m;
            }
            if (noOfBooks > 3) {
                return total * .9m;
            }
            return total;
        }
    }
}
