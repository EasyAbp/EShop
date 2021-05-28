namespace EasyAbp.EShop.Inventory.Stocks
{
    using EasyAbp.EShop.Stores.Stores;
    using System;
    using Volo.Abp.Data;

    /// <summary>
    /// 存品接口
    /// </summary>
    public interface IStock : IHasExtraProperties, IMultiStore
    {
        /// <summary>
        /// 锁定库存数量.
        /// </summary>
        int LockedQuantity { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// 商品SKU
        /// </summary>
        Guid ProductSkuId { get; }

        /// <summary>
        /// 库存数
        /// </summary>
        int Quantity { get; }

        /// <summary>
        /// 排序
        /// </summary>
        int DisplayOrder { get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }
    }
}
