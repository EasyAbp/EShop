using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems;

public interface IBasketItemProductInfoUpdater
{
    Task UpdateProductDataAsync(int targetQuantity, IBasketItem item, ProductDto productDto);
}