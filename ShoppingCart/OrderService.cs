namespace ShoppingCart
{
    public class OrderService : IOrderService
    {
        private readonly IOrderIdGenerator idGenerator;

        public OrderService(IOrderIdGenerator idGenerator)
        {
            this.idGenerator = idGenerator;
        }

        public Order Create(UserId userId, ShoppingCartId cartId)
        {
            var id = this.idGenerator.Next();
            return new Order(id);
        }
    }
}
