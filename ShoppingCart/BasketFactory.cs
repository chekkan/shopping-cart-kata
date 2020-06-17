#nullable enable

using System.IO;

namespace ShoppingCart
{
    public class BasketFactory : IBasketFactory
    {
        private readonly IClock clock;
        private readonly TextWriter writer;

        public BasketFactory(IClock clock, TextWriter writer)
        {
            this.clock = clock;
            this.writer = writer;
        }

        public Basket Create(UserId userId) 
            => new BasketLogger(this.writer, userId, this.clock.Now);
    }
}
