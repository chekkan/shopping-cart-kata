using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ShoppingCart
{
    public class Inventory
    {
        private const int THRESHOLD = 20;
        private List<InventoryItem> items;
        private readonly IPurchaseSystem purchaseSystem;

        public Inventory(IPurchaseSystem purchaseSystem)
        {
            this.items = new List<InventoryItem>();
            this.purchaseSystem = purchaseSystem;
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            this.items.Add(new InventoryItem(productId, quantity, price));
        }

        public bool CheckAvailability(ProductId productId, int quantity)
        {
            return AvailableQuantity(productId) >= quantity;
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
            return GetProduct(productId).Price;
        }

        public void Reserve(ProductId productId, int quantity)
        {
            GetProduct(productId).ReservedCount = quantity;
        }

        public void Sold(ProductId productId, int quantity)
        {
            var product = GetProduct(productId);
            var sellCount = product.ReservedCount - quantity;
            product.ReservedCount = Math.Max(0, sellCount);
            product.Quantity -= quantity;
            if (product.Quantity < THRESHOLD)
            {
                this.purchaseSystem.OrderMore(productId, product.Quantity);
            }
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

        private InventoryItem GetProduct(ProductId productId)
        {
            return this.items.Single(i => i.ProductId == productId);
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
