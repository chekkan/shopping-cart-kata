using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class StockController
    {
        private Dictionary<ProductId, int> stocks;
        private Dictionary<ProductId, decimal> prices;

        public StockController()
        {
            this.stocks = new Dictionary<ProductId, int>();
            this.prices = new Dictionary<ProductId, decimal>()
            {
                {new ProductId(10001), 10m},
                {new ProductId(10002), 5m},
                {new ProductId(20001), 9m},
                {new ProductId(20110), 7m}
            };
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

        public decimal PriceFor(ProductId productId) 
            => prices[productId];
    }
}
