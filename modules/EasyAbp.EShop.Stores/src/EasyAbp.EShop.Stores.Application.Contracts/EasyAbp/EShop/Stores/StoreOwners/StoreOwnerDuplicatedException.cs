using System;
using Volo.Abp;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerDuplicatedException : BusinessException
    {
        public StoreOwnerDuplicatedException(Guid storeId, Guid ownerId) : base(
            message: $"Owner {ownerId} is duplicate for the store {storeId}")
        {
        }
    }
}