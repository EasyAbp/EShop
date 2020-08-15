using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Stores.Permissions
{
    public static class StoreOwnerAuthorizationExtensions
    {
        public static Task<bool> IsStoreOwnerGrantedAsync(this IAuthorizationService authorizationService,
            Guid storeId, string policyName = null, object resource = null)
        {
            return authorizationService.IsGrantedAsync(resource,
                new StorePermissionAuthorizationRequirement(storeId, policyName));
        }

        public static Task CheckStoreOwnerAsync(this IAuthorizationService authorizationService,
            Guid storeId, string policyName = null, object resource = null)
        {
            return authorizationService.CheckAsync(resource,
                new StorePermissionAuthorizationRequirement(storeId, policyName));
        }
    }
}
