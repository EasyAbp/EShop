using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Stores.Authorization
{
    public static class StoreOwnerAuthorizationExtensions
    {
        public static Task<bool> IsCurrentUserGrantedAsync(this IAuthorizationService authorizationService,
            Guid storeId, [CanBeNull] string policyName = null)
        {
            return authorizationService.IsGrantedAsync(new StoreInfo {StoreId = storeId},
                new StorePermissionAuthorizationRequirement(policyName));
        }

        public static async Task<bool> IsMultiStoreGrantedAsync(this IAuthorizationService authorizationService,
            Guid? storeId, [CanBeNull] string policyName, [NotNull] string crossStorePolicyName)
        {
            if (storeId.HasValue && await authorizationService.IsCurrentUserGrantedAsync(storeId.Value, policyName))
            {
                return true;
            }

            return await authorizationService.IsGrantedAsync(crossStorePolicyName)
                   && (policyName.IsNullOrEmpty() || await authorizationService.IsGrantedAsync(policyName));
        }
        
        public static Task CheckMultiStorePolicyAsync(this IAuthorizationService authorizationService,
            Guid storeId, [CanBeNull] string policyName)
        {
            return authorizationService.CheckAsync(new StoreInfo {StoreId = storeId},
                new StorePermissionAuthorizationRequirement(policyName));
        }

        public static async Task CheckMultiStorePolicyAsync(this IAuthorizationService authorizationService,
            Guid? storeId, [CanBeNull] string policyName, [NotNull] string crossStorePolicyName)
        {
            if (storeId.HasValue && await authorizationService.IsCurrentUserGrantedAsync(storeId.Value, policyName))
            {
                return;
            }

            await authorizationService.CheckAsync(crossStorePolicyName);
            
            if (string.IsNullOrEmpty(policyName))
            {
                return;
            }
            
            await authorizationService.CheckAsync(policyName);
        }
    }
}