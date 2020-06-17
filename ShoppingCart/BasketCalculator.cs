using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class BasketCalculator
    {
        private readonly Inventory inventory;

        public BasketCalculator(Inventory inventory)
        {
            this.inventory = inventory;
        }

        public decimal Calculate(IReadOnlyCollection<BasketItem> items)
        {
            var total = items
                .Sum(item => item.Quantity * this.inventory.PriceFor(item.ProductId));
            int noOfBooks = items
                .Where(item => item.ProductId.Type == ProductTypes.Book)
                .Sum(item => item.Quantity);
            int noOfDvds = items
                .Where(item => item.ProductId.Type == ProductTypes.Dvd)
                .Sum(i => i.Quantity);
            if (noOfBooks >= 1 && noOfDvds >= 1) {
                return total * .8m;
            }
            if (noOfBooks > 3) {
                return total * .9m;
            }
            return total;
        }
    }
}
