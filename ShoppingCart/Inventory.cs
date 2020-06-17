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

        public int? QuantityFor(ProductId productId)
        {
            return this.items
                .SingleOrDefault(i => i.ProductId == productId)?.Quantity;
        }

        public decimal PriceFor(ProductId productId)
        {
            return this.items
                .Single(i => i.ProductId == productId).Price;
        }

        public void SetQuantityFor(ProductId productId, int value)
        {
            this.items.SingleOrDefault(i => i.ProductId == productId).Quantity = value;
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
        }
    }
}
