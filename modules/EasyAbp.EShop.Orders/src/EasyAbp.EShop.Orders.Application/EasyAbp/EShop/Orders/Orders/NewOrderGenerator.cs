using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Orders.Orders
{
    public class NewOrderGenerator : INewOrderGenerator, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOrderNumberGenerator _orderNumberGenerator;
        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;
        private readonly IEnumerable<IOrderLinePriceOverrider> _orderLinePriceOverriders;

        public NewOrderGenerator(
            IClock clock,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IServiceProvider serviceProvider,
            IOrderNumberGenerator orderNumberGenerator,
            IProductSkuDescriptionProvider productSkuDescriptionProvider,
            IEnumerable<IOrderLinePriceOverrider> orderLinePriceOverriders)
        {
            _clock = clock;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _serviceProvider = serviceProvider;
            _orderNumberGenerator = orderNumberGenerator;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
            _orderLinePriceOverriders = orderLinePriceOverriders;
        }

        public virtual async Task<Order> GenerateAsync(Guid customerUserId, CreateOrderDto input,
            Dictionary<Guid, ProductDto> productDict, Dictionary<Guid, ProductDetailDto> productDetailDict)
        {
            var orderLines = new List<OrderLine>();

            foreach (var inputOrderLine in input.OrderLines)
            {
                orderLines.Add(await GenerateOrderLineAsync(input, inputOrderLine, productDict, productDetailDict));
            }

            var storeCurrency = await GetStoreCurrencyAsync(input.StoreId);

            if (orderLines.Any(x => x.Currency != storeCurrency))
            {
                throw new UnexpectedCurrencyException(storeCurrency);
            }

            var productTotalPrice = orderLines.Select(x => x.TotalPrice).Sum();
            
            var paymentExpireIn = orderLines.Select(x => productDict[x.ProductId].GetSkuPaymentExpireIn(x.ProductSkuId)).Min();

            var totalPrice = productTotalPrice;
            var totalDiscount = orderLines.Select(x => x.TotalDiscount).Sum();

            var order = new Order(
                id: _guidGenerator.Create(),
                tenantId: _currentTenant.Id,
                storeId: input.StoreId,
                customerUserId: customerUserId,
                currency: storeCurrency,
                productTotalPrice: productTotalPrice,
                totalDiscount: totalDiscount,
                totalPrice: totalPrice,
                actualTotalPrice: totalPrice - totalDiscount,
                customerRemark: input.CustomerRemark,
                paymentExpiration: paymentExpireIn.HasValue ? _clock.Now.Add(paymentExpireIn.Value) : null
            );

            input.MapExtraPropertiesTo(order, MappingPropertyDefinitionChecks.Destination);

            await AddOrderExtraFeesAsync(order, customerUserId, input, productDict);

            order.SetOrderLines(orderLines);

            order.SetOrderNumber(await _orderNumberGenerator.CreateAsync(order));

            return order;
        }

        protected virtual async Task AddOrderExtraFeesAsync(Order order, Guid customerUserId,
            CreateOrderDto input, Dictionary<Guid, ProductDto> productDict)
        {
            var providers = _serviceProvider.GetServices<IOrderExtraFeeProvider>();

            foreach (var provider in providers)
            {
                var infoModels = await provider.GetListAsync(customerUserId, input, productDict);

                foreach (var infoModel in infoModels)
                {
                    order.AddOrderExtraFee(infoModel.Fee, infoModel.Name, infoModel.Key);
                }
            }
        }

        protected virtual async Task<OrderLine> GenerateOrderLineAsync(CreateOrderDto input,
            CreateOrderLineDto inputOrderLine, Dictionary<Guid, ProductDto> productDict,
            Dictionary<Guid, ProductDetailDto> productDetailDict)
        {
            var product = productDict[inputOrderLine.ProductId];
            var productSku = product.GetSkuById(inputOrderLine.ProductSkuId);

            var productDetailId = productSku.ProductDetailId ?? product.ProductDetailId;
            var productDetail = productDetailId.HasValue ? productDetailDict[productDetailId.Value] : null;

            if (!inputOrderLine.Quantity.IsBetween(productSku.OrderMinQuantity, productSku.OrderMaxQuantity))
            {
                throw new OrderLineInvalidQuantityException(product.Id, productSku.Id, inputOrderLine.Quantity);
            }

            var unitPrice = await GetUnitPriceAsync(input, inputOrderLine, product, productSku);
            
            var totalPrice = unitPrice * inputOrderLine.Quantity;

            var orderLine = new OrderLine(
                id: _guidGenerator.Create(),
                productId: product.Id,
                productSkuId: productSku.Id,
                productDetailId: productDetailId,
                productModificationTime: product.LastModificationTime ?? product.CreationTime,
                productDetailModificationTime: productDetail?.LastModificationTime ?? productDetail?.CreationTime,
                productGroupName: product.ProductGroupName,
                productGroupDisplayName: product.ProductGroupDisplayName,
                productUniqueName: product.UniqueName,
                productDisplayName: product.DisplayName,
                skuName: productSku.Name,
                skuDescription: await _productSkuDescriptionProvider.GenerateAsync(product, productSku),
                mediaResources: product.MediaResources,
                currency: productSku.Currency,
                unitPrice: unitPrice,
                totalPrice: totalPrice,
                totalDiscount: 0,
                actualTotalPrice: totalPrice,
                quantity: inputOrderLine.Quantity
            );
            
            inputOrderLine.MapExtraPropertiesTo(orderLine, MappingPropertyDefinitionChecks.Destination);

            return orderLine;
        }

        protected virtual async Task<decimal> GetUnitPriceAsync(CreateOrderDto input, CreateOrderLineDto inputOrderLine,
            ProductDto product, ProductSkuDto productSku)
        {
            foreach (var overrider in _orderLinePriceOverriders)
            {
                var overridenUnitPrice =
                    await overrider.GetUnitPriceOrNullAsync(input, inputOrderLine, product, productSku);

                if (overridenUnitPrice is not null)
                {
                    return overridenUnitPrice.Value;
                }
            }

            return productSku.Price;
        }

        protected virtual Task<string> GetStoreCurrencyAsync(Guid storeId)
        {
            // Todo: Get real store currency configuration.
            return Task.FromResult("USD");
        }
    }
}