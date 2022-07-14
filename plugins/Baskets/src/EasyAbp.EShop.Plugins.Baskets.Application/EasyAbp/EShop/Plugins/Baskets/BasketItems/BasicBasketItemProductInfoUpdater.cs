using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems;

public class BasicBasketItemProductInfoUpdater : IBasketItemProductInfoUpdater, ITransientDependency
{
    protected IProductSkuDescriptionProvider ProductSkuDescriptionProvider { get; }

    public BasicBasketItemProductInfoUpdater(IProductSkuDescriptionProvider productSkuDescriptionProvider)
    {
        ProductSkuDescriptionProvider = productSkuDescriptionProvider;
    }

    public virtual async Task UpdateProductDataAsync(int targetQuantity, IBasketItem item, ProductDto productDto)
    {
        var productSkuDto = productDto.FindSkuById(item.ProductSkuId);

        if (productSkuDto == null)
        {
            item.SetIsInvalid(true);

            return;
        }

        item.UpdateProductData(targetQuantity, new ProductDataModel
        {
            MediaResources = productSkuDto.MediaResources ?? productDto.MediaResources,
            ProductUniqueName = productDto.UniqueName,
            ProductDisplayName = productDto.DisplayName,
            SkuName = productSkuDto.Name,
            SkuDescription = await ProductSkuDescriptionProvider.GenerateAsync(productDto, productSkuDto),
            Currency = productSkuDto.Currency,
            UnitPrice = productSkuDto.DiscountedPrice,
            TotalPrice = productSkuDto.DiscountedPrice * item.Quantity,
            TotalDiscount = (productSkuDto.Price - productSkuDto.DiscountedPrice) * item.Quantity,
            Inventory = productSkuDto.Inventory
        });

        if (productDto.InventoryStrategy != InventoryStrategy.NoNeed && targetQuantity > productSkuDto.Inventory)
        {
            item.SetIsInvalid(true);
        }

        if (!productDto.IsPublished)
        {
            item.SetIsInvalid(true);
        }
    }
}