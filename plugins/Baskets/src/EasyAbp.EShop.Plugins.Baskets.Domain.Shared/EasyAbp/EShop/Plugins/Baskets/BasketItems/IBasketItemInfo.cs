using System;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems;

public interface IBasketItemInfo : IProductData, IHasExtraProperties
{
    Guid Id { get; }

    [NotNull]
    string BasketName { get; }

    Guid StoreId { get; }

    Guid ProductId { get; }

    Guid ProductSkuId { get; }

    int Quantity { get; }

    /// <summary>
    /// PriceWithoutDiscount * Quantity
    /// </summary>
    decimal TotalPriceWithoutDiscount { get; }

    bool IsInvalid { get; }
}