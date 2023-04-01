using System;
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

    public virtual Task UpdateForAnonymousAsync(int targetQuantity, IBasketItem item, ProductDto productDto)
    {
        return InternalUpdateAsync(targetQuantity, item, productDto);
    }

    public virtual Task UpdateForIdentifiedAsync(int targetQuantity, IBasketItem item, ProductDto productDto)
    {
        return InternalUpdateAsync(targetQuantity, item, productDto);
    }

    protected virtual async Task InternalUpdateAsync(int targetQuantity, IBasketItem item, ProductDto productDto)
    {
        var productSkuDto = (ProductSkuDto)productDto.FindSkuById(item.ProductSkuId);

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
            return;
        }

        if (!productDto.IsPublished)
        {
            item.SetIsInvalid(true);
            return;
        }

        if (!targetQuantity.IsBetween(productSkuDto.OrderMinQuantity, productSkuDto.OrderMaxQuantity))
        {
            item.SetIsInvalid(true);
            return;
        }
    }
}