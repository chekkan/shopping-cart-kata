using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class InMemoryBasketRepository : IBasketRepository
    {
        private List<Basket> baskets;

        public InMemoryBasketRepository()
        {
            this.baskets = new List<Basket>();
        }

        public Basket GetBasket(UserId userId)
        {
            return this.baskets.FirstOrDefault(basket => basket.UserId == userId);
        }

        public void Save(Basket basket)
        {
            var existing = GetBasket(basket.UserId);
            if (existing != null)
            {
                this.baskets.Remove(existing);
            }
            this.baskets.Add(basket);
        }
    }
}