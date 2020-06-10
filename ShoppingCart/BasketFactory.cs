#nullable enable

using System.IO;

namespace ShoppingCart
{
    public class BasketFactory : IBasketFactory
    {
        private readonly IClock clock;

        public BasketFactory(IClock clock)
        {
            this.clock = clock;
        }

        public Basket Create(UserId userId) 
            => new BasketLogger(new StringWriter(), userId, clock.Now);
    }
}
