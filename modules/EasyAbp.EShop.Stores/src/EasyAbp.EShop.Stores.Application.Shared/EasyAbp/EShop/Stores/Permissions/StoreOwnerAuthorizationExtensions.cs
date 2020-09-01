using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Stores.Permissions
{
    public static class StoreOwnerAuthorizationExtensions
    {
        public static Task<bool> IsCurrentUserGrantedAsync(this IAuthorizationService authorizationService,
            Guid storeId, string policyName = null, object resource = null)
        {
            return authorizationService.IsGrantedAsync(resource,
                new StorePermissionAuthorizationRequirement(storeId, policyName));
        }

        public static async Task<bool> IsMultiStoreGrantedAsync(this IAuthorizationService authorizationService,
            Guid? storeId, string policyName, string crossStorePolicyName, object resource = null)
        {
            if (storeId.HasValue && await authorizationService.IsCurrentUserGrantedAsync(storeId.Value, policyName))
            {
                return true;
            }

            return await authorizationService.IsGrantedAsync(resource, crossStorePolicyName)
                   && await authorizationService.IsGrantedAsync(resource, policyName);
        }
        
        public static Task CheckMultiStorePolicyAsync(this IAuthorizationService authorizationService,
            Guid storeId, string policyName, object resource = null)
        {
            return authorizationService.CheckAsync(resource,
                new StorePermissionAuthorizationRequirement(storeId, policyName));
        }

        public static async Task CheckMultiStorePolicyAsync(this IAuthorizationService authorizationService,
            Guid? storeId, string policyName, string crossStorePolicyName, object resource = null)
        {
            if (storeId.HasValue && await authorizationService.IsCurrentUserGrantedAsync(storeId.Value, policyName, resource))
            {
                return;
            }

            await authorizationService.CheckAsync(resource, crossStorePolicyName);
            await authorizationService.CheckAsync(resource, policyName);
        }
    }
}