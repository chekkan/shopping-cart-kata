using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class StockController
    {
        private Inventory inventory;

        public StockController(Inventory inventory)
        {
            this.inventory = inventory;
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
    }
}
