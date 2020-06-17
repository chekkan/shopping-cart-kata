using System.Collections.Generic;
using System.IO;

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
            public int Quantity { get; }
        }
    }
}
