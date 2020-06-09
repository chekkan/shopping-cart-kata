using System;

namespace ShoppingCart
{
    public class ShoppingBasketService
    {
        private readonly IBasketRepository basketRepository;

        public ShoppingBasketService(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public void AddItem(UserId userId, ProductId productId, int quantity)
        {
            var basket = this.basketRepository.GetBasket(userId);
            if (basket == null)
            {
                basket = new Basket(userId);
            }
            basket.Add(new BasketItem(productId, quantity));
            this.basketRepository.Save(basket);
        }
    }
}
