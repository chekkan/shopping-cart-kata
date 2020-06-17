#nullable enable

using System.IO;

namespace ShoppingCart
{
    public class BasketFactory : IBasketFactory
    {
        private readonly IClock clock;
        private readonly TextWriter writer;
        private readonly Inventory inventory;

        public BasketFactory(IClock clock, TextWriter writer, Inventory inventory)
        {
            this.clock = clock;
            this.writer = writer;
            this.inventory = inventory;
        }

        public Basket Create(UserId userId) 
            => new BasketLogger(this.writer, userId, this.clock.Now, this.inventory);
    }
}
