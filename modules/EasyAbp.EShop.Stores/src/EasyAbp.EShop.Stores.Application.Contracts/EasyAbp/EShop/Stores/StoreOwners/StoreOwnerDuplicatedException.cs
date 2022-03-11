using System;
using Volo.Abp;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerDuplicatedException : BusinessException
    {
        public StoreOwnerDuplicatedException(Guid storeId, Guid ownerUserId) : base(StoresErrorCodes.StoreOwnerDuplicated)
        {
            WithData(nameof(storeId), storeId);
            WithData(nameof(ownerUserId), ownerUserId);
        }
    }
}