using System;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductSku : IHasAttributeOptionIds, IHasExtraProperties
    {
        Guid Id { get; }

        [CanBeNull]
        string Name { get; }

        [NotNull]
        string Currency { get; }

        decimal? OriginalPrice { get; }

        decimal Price { get; }

        int OrderMinQuantity { get; }

        int OrderMaxQuantity { get; }

        TimeSpan? PaymentExpireIn { get; }

        [CanBeNull]
        string MediaResources { get; }

        Guid? ProductDetailId { get; }
    }
}