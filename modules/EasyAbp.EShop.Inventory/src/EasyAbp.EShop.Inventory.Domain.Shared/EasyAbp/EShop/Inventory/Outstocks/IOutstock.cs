namespace EasyAbp.EShop.Inventory.Outstocks
{
    using EasyAbp.EShop.Stores.Stores;
    using System;

    /// <summary>
    /// 出库单
    /// </summary>
    public interface IOutstock : IMultiStore
    {
        /// <summary>
        /// 出库时间
        /// </summary>
        DateTime OutstockTime { get; }

        /// <summary>
        /// 商品SKU ID
        /// </summary>
        Guid ProductSkuId { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// 单价
        /// </summary>
        decimal UnitPrice { get; }

        /// <summary>
        /// Gets the Units.
        /// </summary>
        int Units { get; }

        /// <summary>
        /// Gets the OperatorName.
        /// </summary>
        string OperatorName { get; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        string Description { get; }
    }
}
