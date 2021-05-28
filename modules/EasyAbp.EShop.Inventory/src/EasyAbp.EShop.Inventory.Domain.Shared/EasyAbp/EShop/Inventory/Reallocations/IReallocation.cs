using EasyAbp.EShop.Stores.Stores;
using System;

namespace EasyAbp.EShop.Inventory.Reallocations
{
    public interface IReallocation : IMultiStore
    {
        Guid ProductSkuId { get; }

        /// <summary>
        /// 商品SPU
        /// </summary>
        Guid ProductId { get; }

        Guid SourceWarehouseId { get; }

        Guid DestinationWarehouseId { get; }

        int Units { get; }

        string OperatorName { get; }

        DateTime ReallocationTime { get; }

        string Description { get; }

    }
}