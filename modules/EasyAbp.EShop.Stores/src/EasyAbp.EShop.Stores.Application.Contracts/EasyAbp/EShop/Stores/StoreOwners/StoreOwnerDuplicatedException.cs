using System;
using Volo.Abp;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerDuplicatedException : BusinessException
    {
        public StoreOwnerDuplicatedException(Guid storeId, Guid ownerUserId) : base(
            message: $"Owner {ownerUserId} is duplicate for the store {storeId}")
        {
        }
    }
}