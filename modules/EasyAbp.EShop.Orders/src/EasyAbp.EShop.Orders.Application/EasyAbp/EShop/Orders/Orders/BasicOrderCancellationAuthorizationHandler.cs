using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Authorization;
using EasyAbp.EShop.Stores.StoreOwners;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Orders.Orders
{
    public class BasicOrderCancellationAuthorizationHandler : OrderCancellationAuthorizationHandler
    {
        private readonly IStoreOwnerStore _storeOwnerStore;
        private readonly IPermissionChecker _permissionChecker;
        private readonly ICurrentUser _currentUser;

        public BasicOrderCancellationAuthorizationHandler(
            IStoreOwnerStore storeOwnerStore,
            IPermissionChecker permissionChecker,
            ICurrentUser currentUser)
        {
            _storeOwnerStore = storeOwnerStore;
            _permissionChecker = permissionChecker;
            _currentUser = currentUser;
        }
        
        protected override async Task HandleOrderCancellationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, Order resource)
        {
            if (!await _permissionChecker.IsGrantedAsync(OrdersPermissions.Orders.Cancel))
            {
                context.Fail();
                return;
            }

            if (resource.CustomerUserId != _currentUser.GetId())
            {
                if (!await _permissionChecker.IsGrantedAsync(OrdersPermissions.Orders.Manage))
                {
                    context.Fail();
                    return;
                }

                if (!await _storeOwnerStore.IsStoreOwnerAsync(resource.StoreId, _currentUser.GetId()) &&
                    !await _permissionChecker.IsGrantedAsync(OrdersPermissions.Orders.CrossStore))
                {
                    context.Fail();
                    return;
                }
            }
            
            if (!resource.IsPaid())
            {
                context.Succeed(requirement);
                return;
            }
        }
    }
}