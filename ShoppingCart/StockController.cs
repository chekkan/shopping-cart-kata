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
            return this.inventory.AvailableQuantity(productId) >= quantity;
        }

        public void Reserve(ProductId productId, int quantity)
        {
            if (this.CheckAvailability(productId, quantity))
            {
                this.inventory.Reserve(productId, quantity);
            }
        }
    }
}
