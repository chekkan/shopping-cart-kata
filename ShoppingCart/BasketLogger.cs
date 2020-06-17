using System;
using System.IO;

namespace ShoppingCart
{
    public class BasketLogger : Basket
    {
        private readonly TextWriter writer;

        public BasketLogger(TextWriter writer,
                            UserId userId,
                            DateTime creationDate,
                            Inventory inventory)
            : base(userId, creationDate, inventory)
        {
            this.writer = writer;
        }

        public override void Add(BasketItem item)
        {
            base.Add(item);
            this.writer.WriteLine($"[ITEM ADDED TO SHOPPING CART]: " +
                $"User[{this.UserId}], " +
                $"Product[{item.ProductId}], " +
                $"Quantity[{item.Quantity}]");
            this.writer.Flush();
        }
    }
}
