using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Orders.Orders
{
    public class NewOrderGenerator : INewOrderGenerator, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly ICurrentUser _currentUser;
        private readonly IOrderNumberGenerator _orderNumberGenerator;
        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;

        public NewOrderGenerator(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ICurrentUser currentUser,
            IOrderNumberGenerator orderNumberGenerator,
            IProductSkuDescriptionProvider productSkuDescriptionProvider)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _currentUser = currentUser;
            _orderNumberGenerator = orderNumberGenerator;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
        }
        
        public virtual async Task<Order> GenerateAsync(CreateOrderDto input, Dictionary<Guid, ProductDto> productDict, Dictionary<string, object> orderExtraProperties)
        {
            var orderLines = new List<OrderLine>();

            foreach (var orderLine in input.OrderLines)
            {
                orderLines.Add(await GenerateOrderLineAsync(orderLine, productDict, orderExtraProperties));
            }

            var productTotalPrice = orderLines.Select(x => x.TotalPrice).Sum();

            // Todo: totalPrice may contain other fee.
            var totalPrice = productTotalPrice;
            var totalDiscount = orderLines.Select(x => x.TotalDiscount).Sum();
            
            var order = new Order(
                id: _guidGenerator.Create(),
                tenantId: _currentTenant.Id,
                storeId: input.StoreId,
                customerUserId: _currentUser.GetId(),
                currency: await GetStoreCurrencyAsync(input.StoreId),
                productTotalPrice: productTotalPrice,
                totalDiscount: totalDiscount,
                totalPrice: totalPrice,
                actualTotalPrice: totalPrice - totalDiscount,
                customerRemark: input.CustomerRemark);

            foreach (var orderExtraProperty in orderExtraProperties)
            {
                order.SetProperty(orderExtraProperty.Key, orderExtraProperty.Value);
            }

            order.SetOrderLines(orderLines);
            
            order.SetOrderNumber(await _orderNumberGenerator.CreateAsync(order));
            
            return order;
        }

        protected virtual async Task<OrderLine> GenerateOrderLineAsync(CreateOrderLineDto input, Dictionary<Guid, ProductDto> productDict, Dictionary<string, object> orderExtraProperties)
        {
            var product = productDict[input.ProductId];
            var productSku = product.GetSkuById(input.ProductSkuId);

            if (!input.Quantity.IsBetween(productSku.OrderMinQuantity, productSku.OrderMaxQuantity))
            {
                throw new OrderLineInvalidQuantityException(product.Id, productSku.Id, input.Quantity);
            }

            var totalPrice = productSku.Price * input.Quantity;
            
            return new OrderLine(
                id: _guidGenerator.Create(),
                productId: product.Id,
                productSkuId: productSku.Id,
                productModificationTime: product.LastModificationTime ?? product.CreationTime,
                productDetailModificationTime: productSku.LastModificationTime ?? productSku.CreationTime,
                productGroupName: product.ProductGroupName,
                productGroupDisplayName: product.ProductGroupDisplayName,
                productUniqueName: product.UniqueName,
                productDisplayName: product.DisplayName,
                skuName: productSku.Name,
                skuDescription: await _productSkuDescriptionProvider.GenerateAsync(product, productSku),
                mediaResources: product.MediaResources,
                currency: productSku.Currency,
                unitPrice: productSku.Price,
                totalPrice: totalPrice,
                totalDiscount: 0,
                actualTotalPrice: totalPrice,
                quantity: input.Quantity
            );
        }

        protected virtual Task<string> GetStoreCurrencyAsync(Guid storeId)
        {
            // Todo: Get real store currency configuration.
            return Task.FromResult("CNY");
        }
    }
}