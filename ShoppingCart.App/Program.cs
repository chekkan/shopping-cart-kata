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

            // var stockController = new StockController(inventory);
        }
    }
}
