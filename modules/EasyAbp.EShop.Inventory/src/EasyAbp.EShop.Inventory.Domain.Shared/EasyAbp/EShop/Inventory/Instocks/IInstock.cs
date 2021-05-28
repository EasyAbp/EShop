namespace EasyAbp.EShop.Inventory.Instocks
{
    using EasyAbp.EShop.Stores.Stores;
    using System;

    /// <summary>
    /// 入库接口
    /// </summary>
    public interface IInstock : IMultiStore
    {
        /// <summary>
        /// 入库时间
        /// </summary>
        DateTime InstockTime { get; }

        /// <summary>
        /// 产品SKU ID
        /// </summary>
        Guid ProductSkuId { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 单价
        /// </summary>
        decimal UnitPrice { get; }

        /// <summary>
        /// 数量
        /// </summary>
        int Units { get; }

        /// <summary>
        /// 入库人
        /// </summary>
        string OperatorName { get; }

        /// <summary>
        /// 供应商 ID
        /// </summary>
        Guid SupplierId { get; }
    }
}
