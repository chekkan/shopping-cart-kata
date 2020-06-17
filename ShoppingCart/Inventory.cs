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

        public void Add(ProductId productId, int quantity)
        {
            this.items.Add(new InventoryItem(productId, quantity));
        }

        public int? QuantityFor(ProductId productId)
        {
            return this.items
                .SingleOrDefault(i => i.ProductId == productId)?.Quantity;
        }

        public void SetQuantityFor(ProductId productId, int value)
        {
            this.items.SingleOrDefault(i => i.ProductId == productId).Quantity = value;
        }

        public void Print(TextWriter writer)
        {
            foreach(var item in this.items)
            {
                writer.WriteLine($"{item.Quantity} x ProductId({item.ProductId})");
            }
        }

        private class InventoryItem
        {
            public InventoryItem(ProductId productId, int quantity)
            {
                this.ProductId = productId;
                this.Quantity = quantity;
            }

            public ProductId ProductId { get; }
            public int Quantity { get; set; }
        }
    }
}
