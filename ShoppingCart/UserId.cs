namespace ShoppingCart
{
    public class UserId
    {
        private readonly string id;

        public UserId(string id)
        {
            this.id = id;
        }

        public override string ToString() => this.id;
    }
}
