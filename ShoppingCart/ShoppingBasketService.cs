namespace ShoppingCart
{
    public class ShoppingBasketService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IBasketFactory basketFactory;

        public ShoppingBasketService(IBasketRepository basketRepository,
                                     IBasketFactory basketFactory)
        {
            this.basketRepository = basketRepository;
            this.basketFactory = basketFactory;
        }

        public void AddItem(UserId userId,
                            ProductId productId,
                            int quantity)
        {
            var basket = this.basketRepository.GetBasket(userId);
            if (basket == null)
            {
                basket = this.basketFactory.Create(userId);
            }
            basket.Add(new BasketItem(productId, quantity));
            this.basketRepository.Save(basket);
        }
    }
}
