﻿using System;

namespace ShoppingCart.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var lordOfTheRings = new ProductId(10001);
            var theHobbit = new ProductId(10002);
            var gameOfThrones = new ProductId(20001);
            var breakingBad = new ProductId(20110);

            var inventory = new Inventory();
            inventory.Add(lordOfTheRings, 10);
            inventory.Add(theHobbit, 12);
            inventory.Add(gameOfThrones, 8);
            inventory.Add(breakingBad, 14);
            inventory.Print(Console.Out);

            var john = new UserId("john");
            var kim = new UserId("kim");

            var basketRepository = new InMemoryBasketRepository();
            var basketFactory = new BasketFactory(new SystemClock(), Console.Out);
            var stockController = new StockController(inventory);
            var shoppingBasketService = new ShoppingBasketService(
                basketRepository,
                basketFactory,
                stockController);

            // No discounts
            shoppingBasketService.AddItem(john, lordOfTheRings, 1);
            shoppingBasketService.AddItem(john, breakingBad, 1);

            shoppingBasketService.AddItem(kim, theHobbit, 1);

            // var johnsCart = shoppingBasketService.BasketFor(john);
            // var kimsCart = shoppingBasketService.BasketFor(kim);

            var orderService = new OrderService(new OrderIdGenerator());
            // orderService.Create(john, johnsCart.Id);
        }
    }

    public class OrderIdGenerator : IOrderIdGenerator
    {
        public OrderId Next()
        {
            return new OrderId(Guid.NewGuid().ToString().ToLower());
        }
    }

    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
