using System;

namespace EasyAbp.EShop.Stores.Stores
{
    public interface IMultiStore
    {
        Guid? StoreId { get; }
    }
}