using System;

namespace ShoppingCart
{
    public class ShoppingBasketService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IBasketFactory basketFactory;
        private readonly StockController stockController;

        public ShoppingBasketService(IBasketRepository basketRepository,
                                     IBasketFactory basketFactory,
                                     StockController stockController)
        {
            this.basketRepository = basketRepository;
            this.basketFactory = basketFactory;
            this.stockController = stockController;
        }

        public void AddItem(UserId userId,
                            ProductId productId,
                            int quantity)
        {
            if (!this.stockController.CheckAvailability(productId, quantity))
            {
                throw new OutOfStockException();
            }
            var basket = this.basketRepository.GetBasket(userId);
            if (basket == null)
            {
                basket = this.basketFactory.Create(userId);
            }
            basket.Add(new BasketItem(productId, quantity));
            this.basketRepository.Save(basket);
            this.stockController.Reserve(productId, quantity);
        }

        public Basket BasketFor(UserId userId)
            => this.basketRepository.GetBasket(userId);
    }
}
