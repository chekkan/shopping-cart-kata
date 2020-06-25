using System;

namespace ShoppingCart.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var productSystem = new PretendPurchaseSystem();

            var lordOfTheRings = new ProductId(10001);
            var theHobbit = new ProductId(10002);
            var gameOfThrones = new ProductId(20001);
            var breakingBad = new ProductId(20110);

            var inventory = new Inventory(productSystem);
            inventory.Add(lordOfTheRings, 10, 10m);
            inventory.Add(theHobbit, 12, 5m);
            inventory.Add(gameOfThrones, 8, 9m);
            inventory.Add(breakingBad, 14, 7m);
            inventory.Print(Console.Out);

            var john = new UserId("john");
            var kim = new UserId("kim");

            var basketRepository = new InMemoryBasketRepository();
            var basketFactory = new BasketFactory(new SystemClock(),
                                                  Console.Out,
                                                  inventory);
            var shoppingBasketService = new ShoppingBasketService(
                basketRepository,
                basketFactory,
                inventory);

            // No discounts
            shoppingBasketService.AddItem(john, lordOfTheRings, 1);
            shoppingBasketService.AddItem(john, theHobbit, 1);

            shoppingBasketService.AddItem(kim, breakingBad, 1);

            var johnsCart = shoppingBasketService.BasketFor(john);
            var kimsCart = shoppingBasketService.BasketFor(kim);

            Console.WriteLine($"John's basket total: {johnsCart.Total:c}");
            Console.WriteLine($"Kim's basket total: {kimsCart.Total:c}");

            var johnsPayment = new PaymentDetails();
            var orderService = new OrderService(new OrderIdGenerator(), basketRepository);
            var paymentGateway = new PretendPaymentGateway();
            var paymentService = new PaymentService(orderService, paymentGateway, inventory);
            paymentService.MakePayment(john, johnsCart.Id, johnsPayment);

            inventory.Print(Console.Out);
        }
    }

    public class PretendPurchaseSystem : IPurchaseSystem
    {
        public void OrderMore(ProductId productId, int actualQuantity)
        {
        }
    }

    public class PretendPaymentGateway : IPaymentGateway
    {
        public PaymentReference Pay(Order order, UserId userId, PaymentDetails payment)
        {
            Console.WriteLine($"Received payment from {userId}.");
            return new PaymentReference();
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
