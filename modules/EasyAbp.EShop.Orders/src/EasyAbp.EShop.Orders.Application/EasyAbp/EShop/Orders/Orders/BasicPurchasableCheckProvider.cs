using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders
{
    public class BasicPurchasableCheckProvider : IPurchasableCheckProvider, ITransientDependency
    {
        public virtual async Task CheckAsync(CreateOrderDto input, Dictionary<Guid, ProductDto> productDict)
        {
            await CheckProductsPublishedAsync(input, productDict);

            await CheckInventoriesSufficientAsync(input, productDict);
        }

        protected virtual Task CheckProductsPublishedAsync(CreateOrderDto input,
            Dictionary<Guid, ProductDto> productDict)
        {
            foreach (var productId in input.OrderLines.Select(dto => dto.ProductId).Distinct().ToArray())
            {
                if (!productDict[productId].IsPublished)
                {
                    throw new NotPurchasableException(productId, null, "Unpublished project");
                }
            }
            
            return Task.CompletedTask;
        }

        protected virtual Task CheckInventoriesSufficientAsync(CreateOrderDto input,
            Dictionary<Guid, ProductDto> productDict)
        {
            foreach (var orderLine in input.OrderLines)
            {
                var product = productDict[orderLine.ProductId];
                var inventory = product.ProductSkus
                    .Single(sku => sku.Id == orderLine.ProductSkuId).Inventory;

                if (product.InventoryStrategy != InventoryStrategy.NoNeed && inventory < orderLine.Quantity)
                {
                    throw new NotPurchasableException(orderLine.ProductId, orderLine.ProductSkuId,
                        "Insufficient inventory");
                }
            }
            
            return Task.CompletedTask;
        }
    }
}