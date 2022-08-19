using System;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderLine : IHasExtraProperties
    {
        Guid ProductId { get; }

        Guid ProductSkuId { get; }

        Guid? ProductDetailId { get; }

        DateTime ProductModificationTime { get; }

        DateTime? ProductDetailModificationTime { get; }

        string ProductGroupName { get; }

        string ProductGroupDisplayName { get; }

        string ProductUniqueName { get; }

        string ProductDisplayName { get; }

        /// <summary>
        /// If it is <c>null</c>, should get the <see cref="InventoryStrategy"/> from the Product entity.
        /// See https://github.com/EasyAbp/EShop/issues/215
        /// </summary>
        InventoryStrategy? ProductInventoryStrategy { get; }

        string SkuName { get; }

        string SkuDescription { get; }

        string MediaResources { get; }

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
    }
}