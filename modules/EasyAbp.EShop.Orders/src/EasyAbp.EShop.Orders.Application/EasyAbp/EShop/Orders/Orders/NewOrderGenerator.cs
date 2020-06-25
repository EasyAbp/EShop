using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
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
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IOrderNumberGenerator _orderNumberGenerator;

        public NewOrderGenerator(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ICurrentUser currentUser,
            IJsonSerializer jsonSerializer,
            IOrderNumberGenerator orderNumberGenerator)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _currentUser = currentUser;
            _jsonSerializer = jsonSerializer;
            _orderNumberGenerator = orderNumberGenerator;
        }
        
        public virtual async Task<Order> GenerateAsync(CreateOrderDto input, Dictionary<Guid, ProductDto> productDict)
        {
            var orderLines = new List<OrderLine>();

            foreach (var orderLine in input.OrderLines)
            {
                orderLines.Add(await GenerateNewOrderLineAsync(orderLine, productDict));
            }

            var productTotalPrice = orderLines.Select(x => x.TotalPrice).Sum();
            
            var order = new Order(
                id: _guidGenerator.Create(),
                tenantId: _currentTenant.Id,
                storeId: input.StoreId,
                customerUserId: _currentUser.GetId(),
                currency: await GetStoreCurrencyAsync(input.StoreId),
                productTotalPrice: productTotalPrice,
                totalDiscount: orderLines.Select(x => x.TotalDiscount).Sum(),
                totalPrice: productTotalPrice,
                refundedAmount: 0,
                customerRemark: input.CustomerRemark);

            order.SetOrderLines(orderLines);
            
            order.SetOrderNumber(await _orderNumberGenerator.CreateAsync(order));
            
            return order;
        }

        protected virtual async Task<OrderLine> GenerateNewOrderLineAsync(CreateOrderLineDto input, Dictionary<Guid, ProductDto> productDict)
        {
            var product = productDict[input.ProductId];
            var productSku = product.ProductSkus.Single(x => x.Id == input.ProductSkuId);

            if (!input.Quantity.IsBetween(productSku.OrderMinQuantity, productSku.OrderMaxQuantity))
            {
                throw new OrderLineInvalidQuantityException(product.Id, productSku.Id, input.Quantity);
            }
            
            return new OrderLine(
                id: _guidGenerator.Create(),
                productId: product.Id,
                productSkuId: productSku.Id,
                productModificationTime: product.LastModificationTime ?? product.CreationTime,
                productDetailModificationTime: productSku.LastModificationTime ?? productSku.CreationTime,
                productName: product.DisplayName,
                skuDescription: await GenerateSkuDescriptionAsync(product, productSku),
                mediaResources: product.MediaResources,
                currency: productSku.Currency,
                unitPrice: productSku.Price,
                totalPrice: productSku.Price * input.Quantity,
                totalDiscount: 0,
                quantity: input.Quantity
            );
        }

        protected virtual Task<string> GenerateSkuDescriptionAsync(ProductDto product, ProductSkuDto productSku)
        {
            var names = new Collection<string[]>();

            foreach (var attributeOptionId in productSku.AttributeOptionIds)
            {
                names.Add(product.ProductAttributes.SelectMany(
                    attribute => attribute.ProductAttributeOptions.Where(option => option.Id == attributeOptionId),
                    (attribute, option) => new [] {attribute.DisplayName, option.DisplayName}).Single());
            }

            return Task.FromResult(_jsonSerializer.Serialize(names));
        }
        
        protected virtual Task<string> GetStoreCurrencyAsync(Guid storeId)
        {
            // Todo: Get real store currency configuration.
            return Task.FromResult("CNY");
        }
    }
}