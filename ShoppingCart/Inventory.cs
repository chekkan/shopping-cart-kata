using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShoppingCart
{
    public class Inventory
    {
        private List<InventoryItem> items;

        public Inventory()
        {
            this.items = new List<InventoryItem>();
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            this.items.Add(new InventoryItem(productId, quantity, price));
        }

        public int AvailableQuantity(ProductId productId)
        {
            var product = this.items.SingleOrDefault(i => i.ProductId == productId);
            return product != null
                ? product.Quantity - product.ReservedCount
                : 0;
        }

        public decimal PriceFor(ProductId productId)
        {
            return this.items
                .Single(i => i.ProductId == productId).Price;
        }

        public void Reserve(ProductId productId, int value)
        {
            this.items.Single(i => i.ProductId == productId)
                .ReservedCount = value;
        }

        public void Print(TextWriter writer)
        {
            foreach(var item in this.items)
            {
                writer.Write($"ProductId({item.ProductId}) | ");
                writer.Write($"{item.Price:c} | ".PadLeft(9, ' '));
                writer.WriteLine($"{item.Quantity}");
            }
        }

        private class InventoryItem
        {
            public InventoryItem(ProductId productId, int quantity, decimal price)
            {
                this.ProductId = productId;
                this.Quantity = quantity;
                this.Price = price;
            }

            public ProductId ProductId { get; }
            public decimal Price { get; }
            public int Quantity { get; set; }
            public int ReservedCount { get; set; }
        }
    }
}
