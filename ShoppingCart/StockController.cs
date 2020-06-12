using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class StockController
    {
        private Dictionary<ProductId, int> stocks;
        
        public StockController()
        {
            this.stocks = new Dictionary<ProductId, int>();
        }

        public void AddProduct(ProductId productId, int quantity)
        {
            this.stocks.Add(productId, quantity);
        }

        public bool CheckAvailability(ProductId productId, int quantity)
        {
            var product = this.stocks.FirstOrDefault(stock => stock.Key == productId);
            var stockQty = product.Value;
            return stockQty >= quantity;
        }

        public void Reserve(ProductId productId, int quantity)
        {
            var product = this.stocks.First(stock => stock.Key == productId);
            this.stocks[product.Key] -= quantity;
        }

        internal decimal PriceFor(ProductId productId)
        {
            return productId == new ProductId(10001)
                ? 10m
                : 5m;
        }
    }
}
