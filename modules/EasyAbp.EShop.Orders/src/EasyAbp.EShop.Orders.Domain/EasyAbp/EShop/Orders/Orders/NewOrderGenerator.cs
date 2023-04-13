using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Settings;
using EasyAbp.EShop.Products.Products;
using NodaMoney;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Orders.Orders
{
    public class NewOrderGenerator : DomainService, INewOrderGenerator
    {
        private readonly ISettingProvider _settingProvider;
        private readonly IOrderNumberGenerator _orderNumberGenerator;
        private readonly IProductSkuDescriptionProvider _productSkuDescriptionProvider;
        private readonly IEnumerable<IOrderLinePriceOverrider> _orderLinePriceOverriders;

        public NewOrderGenerator(
            ISettingProvider settingProvider,
            IOrderNumberGenerator orderNumberGenerator,
            IProductSkuDescriptionProvider productSkuDescriptionProvider,
            IEnumerable<IOrderLinePriceOverrider> orderLinePriceOverriders)
        {
            _settingProvider = settingProvider;
            _orderNumberGenerator = orderNumberGenerator;
            _productSkuDescriptionProvider = productSkuDescriptionProvider;
            _orderLinePriceOverriders = orderLinePriceOverriders;
        }

        public virtual async Task<Order> GenerateAsync(Guid customerUserId, ICreateOrderInfo input,
            Dictionary<Guid, IProduct> productDict, Dictionary<Guid, DateTime> productDetailModificationTimeDict)
        {
            await ValidateInputAsync(input);

            var effectiveCurrency = await GetEffectiveCurrencyAsync();

            var orderLines = new List<OrderLine>();

            foreach (var inputOrderLine in input.OrderLines)
            {
                orderLines.Add(await GenerateOrderLineAsync(
                    input, inputOrderLine, productDict, productDetailModificationTimeDict, effectiveCurrency));
            }

            var productTotalPrice = orderLines.Select(x => x.TotalPrice).Sum();

            var paymentExpireIn = orderLines.Select(x => productDict[x.ProductId].GetSkuPaymentExpireIn(x.ProductSkuId))
                .Min();

            var totalPrice = productTotalPrice;
            var totalDiscount = orderLines.Select(x => x.TotalDiscount).Sum();

            var order = new Order(
                id: GuidGenerator.Create(),
                tenantId: CurrentTenant.Id,
                storeId: input.StoreId,
                customerUserId: customerUserId,
                currency: effectiveCurrency.Code,
                productTotalPrice: productTotalPrice,
                totalDiscount: totalDiscount,
                totalPrice: totalPrice,
                actualTotalPrice: totalPrice - totalDiscount,
                customerRemark: input.CustomerRemark,
                paymentExpiration: paymentExpireIn.HasValue ? Clock.Now.Add(paymentExpireIn.Value) : null
            );

            input.MapExtraPropertiesTo(order, MappingPropertyDefinitionChecks.Destination);

            await AddOrderExtraFeesAsync(order, customerUserId, input, productDict, effectiveCurrency);

            order.SetOrderLines(orderLines);

            order.SetOrderNumber(await _orderNumberGenerator.CreateAsync(order));

            // set ReducedInventoryAfterPlacingTime directly if an order contains no OrderLine with `InventoryStrategy.ReduceAfterPlacing`.
            // see https://github.com/EasyAbp/EShop/issues/214
            if (order.OrderLines.All(x => x.ProductInventoryStrategy != InventoryStrategy.ReduceAfterPlacing))
            {
                order.SetReducedInventoryAfterPlacingTime(Clock.Now);
            }

            await DiscountOrderAsync(order, productDict);

            return order;
        }

        protected virtual Task ValidateInputAsync(ICreateOrderInfo info)
        {
            if (!info.OrderLines.Any())
            {
                throw new BusinessException(OrdersErrorCodes.OrderLinesShouldNotBeEmpty);
            }

            if (info.OrderLines.Any(orderLine => orderLine.Quantity < 1))
            {
                throw new BusinessException(OrdersErrorCodes.QuantityShouldBeGreaterThanZero);
            }

            return Task.CompletedTask;
        }

        protected virtual async Task DiscountOrderAsync(Order order, Dictionary<Guid, IProduct> productDict)
        {
            var context = new OrderDiscountContext(order, productDict);

            foreach (var provider in LazyServiceProvider.LazyGetService<IEnumerable<IOrderDiscountProvider>>()
                         .OrderBy(x => x.EffectOrder))
            {
                await provider.DiscountAsync(context);
            }

            var effectDiscounts = context.GetEffectDiscounts();

            foreach (var discount in effectDiscounts)
            {
                order.AddDiscounts(discount);
            }
        }

        protected virtual async Task AddOrderExtraFeesAsync(Order order, Guid customerUserId,
            ICreateOrderInfo input, Dictionary<Guid, IProduct> productDict, Currency effectiveCurrency)
        {
            var providers = LazyServiceProvider.LazyGetService<IEnumerable<IOrderExtraFeeProvider>>();

            foreach (var provider in providers)
            {
                var infoModels = await provider.GetListAsync(customerUserId, input, productDict, effectiveCurrency);

                foreach (var infoModel in infoModels)
                {
                    var fee = new Money(infoModel.Fee, effectiveCurrency);
                    order.AddOrderExtraFee(fee.Amount, infoModel.Name, infoModel.Key, infoModel.DisplayName);
                }
            }
        }

        protected virtual async Task<OrderLine> GenerateOrderLineAsync(ICreateOrderInfo input,
            ICreateOrderLineInfo inputOrderLine, Dictionary<Guid, IProduct> productDict,
            Dictionary<Guid, DateTime> productDetailModificationTimeDict, Currency effectiveCurrency)
        {
            var product = productDict[inputOrderLine.ProductId];
            var productSku = product.GetSkuById(inputOrderLine.ProductSkuId);

            if (productSku.Currency != effectiveCurrency.Code)
            {
                throw new UnexpectedCurrencyException(effectiveCurrency.Code);
            }

            var productDetailId = productSku.ProductDetailId ?? product.ProductDetailId;
            var productDetailModificationTime = productDetailId.HasValue
                ? productDetailModificationTimeDict[productDetailId.Value]
                : (DateTime?)null;

            if (!inputOrderLine.Quantity.IsBetween(productSku.OrderMinQuantity, productSku.OrderMaxQuantity))
            {
                throw new OrderLineInvalidQuantityException(product.Id, productSku.Id, inputOrderLine.Quantity);
            }

            var unitPrice = await GetUnitPriceAsync(input, inputOrderLine, product, productSku, effectiveCurrency);

            var totalPrice = unitPrice * inputOrderLine.Quantity;

            var orderLine = new OrderLine(
                id: GuidGenerator.Create(),
                productId: product.Id,
                productSkuId: productSku.Id,
                productDetailId: productDetailId,
                productModificationTime: product is IAuditedObject auditedProduct
                    ? auditedProduct.LastModificationTime ?? auditedProduct.CreationTime
                    : Clock.Now,
                productDetailModificationTime: productDetailModificationTime,
                productGroupName: product.ProductGroupName,
                productGroupDisplayName: product is IHasProductGroupDisplayName hasProductGroupDisplayName
                    ? hasProductGroupDisplayName.ProductGroupDisplayName
                    : product.ProductGroupName,
                productUniqueName: product.UniqueName,
                productDisplayName: product.DisplayName,
                productInventoryStrategy: product.InventoryStrategy,
                skuName: productSku.Name,
                skuDescription: await _productSkuDescriptionProvider.GenerateAsync(product, productSku),
                mediaResources: product.MediaResources,
                currency: productSku.Currency,
                unitPrice: unitPrice.Amount,
                totalPrice: totalPrice.Amount,
                totalDiscount: 0,
                actualTotalPrice: totalPrice.Amount,
                quantity: inputOrderLine.Quantity
            );

            inputOrderLine.MapExtraPropertiesTo(orderLine, MappingPropertyDefinitionChecks.Destination);

            return orderLine;
        }

        protected virtual async Task<Money> GetUnitPriceAsync(ICreateOrderInfo input,
            ICreateOrderLineInfo inputOrderLine, IProduct product, IProductSku productSku, Currency effectiveCurrency)
        {
            foreach (var overrider in _orderLinePriceOverriders)
            {
                var overridenUnitPrice =
                    await overrider.GetUnitPriceOrNullAsync(input, inputOrderLine, product, productSku,
                        effectiveCurrency);

                if (overridenUnitPrice is not null)
                {
                    return overridenUnitPrice.Value;
                }
            }

            return new Money(productSku.Price, effectiveCurrency);
        }

        protected virtual async Task<Currency> GetEffectiveCurrencyAsync()
        {
            var currencyCode = Check.NotNullOrWhiteSpace(
                await _settingProvider.GetOrNullAsync(OrdersSettings.CurrencyCode),
                nameof(OrdersSettings.CurrencyCode)
            );

            return Currency.FromCode(currencyCode);
        }
    }
}