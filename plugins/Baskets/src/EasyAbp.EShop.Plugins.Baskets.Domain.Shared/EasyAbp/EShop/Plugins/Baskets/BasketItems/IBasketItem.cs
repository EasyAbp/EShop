namespace EasyAbp.EShop.Plugins.Baskets.BasketItems;

public interface IBasketItem : IBasketItemInfo
{
    void SetIsInvalid(bool isInvalid);

    void Update(int quantity, IProductData productData);
}