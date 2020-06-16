using System.Collections.Generic;

namespace ShoppingCart
{
    public class OrderId
    {
        private string id;

        public OrderId(string id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is OrderId id &&
                   this.id == id.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + EqualityComparer<string>.Default.GetHashCode(id);
        }

        public override string ToString() => $"{nameof(OrderId)}({this.id})";
    }
}
