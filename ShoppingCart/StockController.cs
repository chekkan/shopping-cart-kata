using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class StockController
    {
        private Dictionary<ProductId, int> stocks;
        private Inventory inventory;
        private Dictionary<ProductId, decimal> prices;

        public StockController(Inventory inventory)
        {
            this.inventory = inventory;
            this.stocks = new Dictionary<ProductId, int>();
            this.prices = new Dictionary<ProductId, decimal>()
            {
                {new ProductId(10001), 10m},
                {new ProductId(10002), 5m},
                {new ProductId(20001), 9m},
                {new ProductId(20110), 7m}
            };
        }

        public bool CheckAvailability(ProductId productId, int quantity)
        {
            var stockQty = this.inventory.QuantityFor(productId);
            return stockQty != null && stockQty >= quantity;
        }

        public void Reserve(ProductId productId, int quantity)
        {
            var stockQty = this.inventory.QuantityFor(productId);
            if (stockQty != null)
            {
                this.inventory.SetQuantityFor(productId, stockQty.Value - quantity);
            }
        }

        public decimal PriceFor(ProductId productId) 
            => prices[productId];
    }
}
