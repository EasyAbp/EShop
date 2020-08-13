using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public interface IStoreOwnerStore
    {
        Task<bool> IsStoreOwnerAsync(Guid storeId, Guid userId);
    }
}