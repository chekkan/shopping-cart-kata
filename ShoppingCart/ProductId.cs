namespace ShoppingCart
{
    public class ProductId
    {
        private readonly int id;

        public ProductId(int id)
        {
            this.id = id;
        }

        public override string ToString() => this.id.ToString();
    }
}
