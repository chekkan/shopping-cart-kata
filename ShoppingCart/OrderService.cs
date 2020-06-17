namespace ShoppingCart
{
    public class OrderService : IOrderService
    {
        private readonly IOrderIdGenerator idGenerator;
        private readonly IBasketRepository basketRepository;

        public OrderService(IOrderIdGenerator idGenerator,
                            IBasketRepository basketRepository)
        {
            this.idGenerator = idGenerator;
            this.basketRepository = basketRepository;
        }

        public Order Create(UserId userId, ShoppingCartId cartId)
        {
            var basket = this.basketRepository.GetBasket(userId);
            var id = this.idGenerator.Next();
            return new Order(id, basket);
        }
    }
}
