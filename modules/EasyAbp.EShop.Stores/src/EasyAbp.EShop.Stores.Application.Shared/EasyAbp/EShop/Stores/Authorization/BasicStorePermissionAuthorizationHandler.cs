using System;
using EasyAbp.EShop.Stores.StoreOwners;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;

namespace EasyAbp.EShop.Stores.Authorization
{
    public class BasicStorePermissionAuthorizationHandler : AuthorizationHandler<StorePermissionAuthorizationRequirement, StoreInfo>
    {
        private readonly IStoreOwnerStore _storeOwnerStore;
        private readonly IPermissionChecker _permissionChecker;

        public BasicStorePermissionAuthorizationHandler(IStoreOwnerStore storeOwnerStore,
            IPermissionChecker permissionChecker)
        {
            _storeOwnerStore = storeOwnerStore;
            _permissionChecker = permissionChecker;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, StorePermissionAuthorizationRequirement requirement, StoreInfo storeInfo)
        {
            var userId = context.User?.FindUserId();
            if (userId.HasValue)
            {
                var isStoreOwner = await _storeOwnerStore.IsStoreOwnerAsync(storeInfo.StoreId, userId.Value);

                if (isStoreOwner)
                {
                    if (!requirement.PolicyName.IsNullOrWhiteSpace())
                    {
                        if (await _permissionChecker.IsGrantedAsync(context.User, requirement.PolicyName))
                        {
                            context.Succeed(requirement);
                        }
                    }
                    else
                    {
                        context.Succeed(requirement);
                    }
                }
            }
        }
    }
}