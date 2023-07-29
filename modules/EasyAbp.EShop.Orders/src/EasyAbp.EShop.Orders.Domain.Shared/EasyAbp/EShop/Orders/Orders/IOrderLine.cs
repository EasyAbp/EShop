using System;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderLine : IHasExtraProperties, IHasProductGroupDisplayName
    {
        Guid Id { get; }

        Guid ProductId { get; }

        Guid ProductSkuId { get; }

        Guid? ProductDetailId { get; }

        DateTime ProductModificationTime { get; }

        DateTime? ProductDetailModificationTime { get; }

        [NotNull]
        string ProductGroupName { get; }

        [CanBeNull]
        string ProductUniqueName { get; }

        [NotNull]
        string ProductDisplayName { get; }

        /// <summary>
        /// If it is <c>null</c>, should get the <see cref="InventoryStrategy"/> from the Product entity.
        /// See https://github.com/EasyAbp/EShop/issues/215
        /// </summary>
        InventoryStrategy? ProductInventoryStrategy { get; }

        [CanBeNull]
        string SkuName { get; }

        [CanBeNull]
        string SkuDescription { get; }

        [CanBeNull]
        string MediaResources { get; }

        [NotNull]
        string Currency { get; }

        decimal UnitPrice { get; }

        decimal TotalPrice { get; }

        decimal TotalDiscount { get; }

        /// <summary>
        /// ActualTotalPrice = TotalPrice - TotalDiscount
        /// </summary>
        decimal ActualTotalPrice { get; }

        int Quantity { get; }

        int RefundedQuantity { get; }

        decimal RefundAmount { get; }

        decimal? PaymentAmount { get; }
    }
}