namespace ShoppingCart
{
    public class ProductId
    {
        private readonly int id;

        public ProductTypes Type => id.ToString().StartsWith("2") 
            ? ProductTypes.Dvd 
            : ProductTypes.Book;

        public ProductId(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj) 
            => obj is ProductId && this == (ProductId)obj;

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        public static bool operator ==(ProductId x, ProductId y) 
            => x.id == y.id;

        public static bool operator !=(ProductId x, ProductId y) 
            => !(x == y);

        public override string ToString() => this.id.ToString();
    }

    public enum ProductTypes
    {
        Book,
        Dvd
    }
}
