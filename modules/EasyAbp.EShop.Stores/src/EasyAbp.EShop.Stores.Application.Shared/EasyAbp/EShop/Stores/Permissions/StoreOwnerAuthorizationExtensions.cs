using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Authorization;

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

        public static async Task<bool> IsMultiStoreGrantedAsync(this IAuthorizationService authorizationService,
            Guid? storeId, string crossStorePolicyName, string policyName, object resource = null)
        {
            if (storeId.HasValue
                && await authorizationService.IsStoreOwnerGrantedAsync(storeId.Value, policyName))
            {
                return true;
            }

            return await authorizationService.IsGrantedAsync(resource, crossStorePolicyName)
                   && await authorizationService.IsGrantedAsync(resource, policyName);
        }

        public static async Task CheckMultiStorePolicyAsync(this IAuthorizationService authorizationService,
            Guid? storeId, string crossStorePolicyName, string policyName, object resource = null)
        {
            if (storeId.HasValue
                && await authorizationService.IsStoreOwnerGrantedAsync(storeId.Value, policyName))
            {
                return;
            }

            await authorizationService.CheckAsync(resource, crossStorePolicyName);
            await authorizationService.CheckAsync(resource, policyName);
        }

        public static Task CheckStoreOwnerAsync(this IAuthorizationService authorizationService,
            Guid storeId, string policyName = null, object resource = null)
        {
            return authorizationService.CheckAsync(resource,
                new StorePermissionAuthorizationRequirement(storeId, policyName));
        }
    }
}