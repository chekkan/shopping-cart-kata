#nullable enable

using System;

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
            => new Basket(userId, clock.Now);
    }
}
