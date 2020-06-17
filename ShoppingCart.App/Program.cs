using System;

namespace ShoppingCart.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var inventory = new Inventory();
            inventory.Add(new ProductId(10001), 10);
            inventory.Add(new ProductId(10002), 12);
            inventory.Add(new ProductId(20001), 8);
            inventory.Add(new ProductId(20110), 14);
            inventory.Print(Console.Out);

            var basketRepository = new InMemoryBasketRepository();
            var basketFactory = new BasketFactory(new SystemClock());
            var stockController = new StockController(inventory);
            var shoppingBasketService = new ShoppingBasketService(basketRepository, basketFactory, stockController);
        }
    }

    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
