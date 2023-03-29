using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Authorization;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Authorization.Permissions;

namespace EasyAbp.EShop.Orders.Orders
{
    public class BasicOrderCreationAuthorizationHandler : OrderCreationAuthorizationHandler
    {
        private readonly IPermissionChecker _permissionChecker;

        public BasicOrderCreationAuthorizationHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }
        
        protected override async Task HandleOrderCreationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, OrderCreationResource resource)
        {
            if (!await _permissionChecker.IsGrantedAsync(OrdersPermissions.Orders.Create))
            {
                context.Fail();
                return;
            }
            
            if (!await IsProductsPublishedAsync(resource.Input, resource.ProductDictionary))
            {
                context.Fail();
                return;
            }

            if (!await IsInventoriesSufficientAsync(resource.Input, resource.ProductDictionary))
            {
                context.Fail();
                return;
            }
            
            context.Succeed(requirement);
        }

        protected virtual Task<bool> IsProductsPublishedAsync(ICreateOrderInfo input,
            Dictionary<Guid, ProductDto> productDictionary)
        {
            return Task.FromResult(
                input.OrderLines.Select(dto => dto.ProductId).Distinct().ToArray()
                    .All(productId => productDictionary[productId].IsPublished)
            );
        }

        protected virtual Task<bool> IsInventoriesSufficientAsync(ICreateOrderInfo input,
            Dictionary<Guid, ProductDto> productDictionary)
        {
            return Task.FromResult(
                !(from orderLine in input.OrderLines
                    let product = productDictionary[orderLine.ProductId]
                    let inventory = product.ProductSkus.Single(sku => sku.Id == orderLine.ProductSkuId).Inventory
                    where product.InventoryStrategy != InventoryStrategy.NoNeed && inventory < orderLine.Quantity
                    select orderLine).Any()
            );
        }
    }
}