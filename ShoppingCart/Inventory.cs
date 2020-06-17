using System.IO;

namespace ShoppingCart
{
    public class Inventory
    {
        private ProductId productId;
        private int quantity;

        public void Add(ProductId productId, int quantity)
        {
            this.productId = productId;
            this.quantity = quantity;
        }

        public void Print(TextWriter writer)
        {
            writer.WriteLine($"{this.quantity} x ProductId({this.productId})");
        }
    }
}
