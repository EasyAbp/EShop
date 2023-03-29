using System;
using EasyAbp.EShop.Stores.Stores;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductBase : IHasExtraProperties, IMultiStore
    {
        Guid Id { get; }

        [NotNull]
        string ProductGroupName { get; }

        Guid? ProductDetailId { get; }

        [CanBeNull]
        string UniqueName { get; }

        [NotNull]
        string DisplayName { get; }

        /// <summary>
        /// Tell your customer what the product is. It is usually shown in the product list.
        /// </summary>
        [CanBeNull]
        string Overview { get; }

        InventoryStrategy InventoryStrategy { get; }

        [CanBeNull]
        string InventoryProviderName { get; }

        [CanBeNull]
        string MediaResources { get; }

        int DisplayOrder { get; }

        bool IsPublished { get; }

        bool IsStatic { get; }

        bool IsHidden { get; }

        TimeSpan? PaymentExpireIn { get; }
    }
}