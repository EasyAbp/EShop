using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems;

public interface IBasketItem : IProductData
{
    Guid Id { get; }
    
    [NotNull]
    string BasketName { get; }
    
    Guid StoreId { get; }
    
    Guid ProductId { get; }
    
    Guid ProductSkuId { get; }
    
    int Quantity { get; }

    bool IsInvalid { get; }

    void SetIsInvalid(bool isInvalid);

    void UpdateProductData(int quantity, IProductData productData);
}