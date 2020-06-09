using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart
{
    public class Basket
    {
        private List<BasketItem> items;

        public Basket(UserId userId)
        {
            items = new List<BasketItem>();
            UserId = userId;
        }

        public UserId UserId { get; }

        public void Add(BasketItem item)
        {
            this.items.Add(item);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Basket;
            if (other == null)
            {
                return false;
            }
            return other.items.SequenceEqual(items) && other.UserId == UserId;
        }

        public override int GetHashCode()
        {
            return items.Select(i => i.GetHashCode())
                .Aggregate((curr, acc) => acc ^ curr);
        }
    }
}
