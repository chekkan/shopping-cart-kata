using System;
using System.IO;

namespace ShoppingCart
{
    public class BasketLogger : Basket
    {
        private readonly TextWriter writer;

        public BasketLogger(TextWriter writer, UserId userId, DateTime creationDate)
            : base(userId, creationDate)
        {
            this.writer = writer;
        }

        public new void Add(BasketItem item)
        {
            base.Add(item);
            this.writer.WriteLine($"[ITEM ADDED TO SHOPPING CART]: " +
                $"User[{this.UserId}], " +
                $"Product[{item.ProductId}], " +
                $"Quantity[{item.Quantity}]");
        }
    }
}
