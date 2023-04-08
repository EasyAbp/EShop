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

        /// <summary>
        /// The official pricing value.
        /// This property is only used for UI.
        /// </summary>
        decimal? OriginalPrice { get; }

        /// <summary>
        /// The realtime price.
        /// </summary>
        decimal Price { get; }

        int OrderMinQuantity { get; }

        int OrderMaxQuantity { get; }

        TimeSpan? PaymentExpireIn { get; }

        [CanBeNull]
        string MediaResources { get; }

        Guid? ProductDetailId { get; }
    }
}