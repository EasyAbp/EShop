namespace EasyAbp.EShop.Inventory.StockHistories
{
    using EasyAbp.EShop.Stores.Stores;
    using System;

    /// <summary>
    /// 库存存品历史记录
    /// </summary>
    public interface IStockHistory : IMultiStore
    {
        /// <summary>
        /// Gets the LockedQuantity
        /// 锁定库存数量.
        /// </summary>
        int LockedQuantity { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// Gets the ProductSkuId.
        /// </summary>
        Guid ProductSkuId { get; }

        /// <summary>
        /// Gets the Quantity.
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// Gets the AdjustedQuantity.
        /// </summary>
        int AdjustedQuantity { get; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        string Description { get; }
    }
}
