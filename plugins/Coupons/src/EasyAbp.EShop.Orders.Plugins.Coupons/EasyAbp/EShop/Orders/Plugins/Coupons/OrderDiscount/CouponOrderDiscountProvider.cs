using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Products.Products;
using NodaMoney;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponOrderDiscountProvider : IOrderDiscountProvider, ITransientDependency
    {
        public static string OrderDiscountName { get; set; } = "Coupon";

        private readonly IClock _clock;
        private readonly ICouponAppService _couponAppService;
        private readonly ICouponLookupService _couponLookupService;
        private readonly ICouponTemplateLookupService _couponTemplateLookupService;

        public CouponOrderDiscountProvider(
            IClock clock,
            ICouponAppService couponAppService,
            ICouponLookupService couponLookupService,
            ICouponTemplateLookupService couponTemplateLookupService)
        {
            _clock = clock;
            _couponAppService = couponAppService;
            _couponLookupService = couponLookupService;
            _couponTemplateLookupService = couponTemplateLookupService;
        }

        public virtual async Task<List<OrderDiscountInfoModel>> GetAllAsync(Order order,
            Dictionary<Guid, IProduct> productDict)
        {
            var couponId = order.GetProperty<Guid?>(CouponsConsts.OrderCouponIdPropertyName);
            if (couponId is null)
            {
                return new List<OrderDiscountInfoModel>();
            }

            var now = _clock.Now;

            var coupon = await _couponLookupService.FindByIdAsync(couponId.Value);

            if (coupon == null || coupon.ExpirationTime.HasValue && coupon.ExpirationTime.Value < now)
            {
                throw new CouponNotFoundOrHasExpiredException();
            }

            if (coupon.OrderId.HasValue)
            {
                throw new CouponHasBeenOccupiedException();
            }

            var couponTemplate = await _couponTemplateLookupService.FindByIdAsync(coupon.CouponTemplateId);

            if (couponTemplate == null ||
                !IsInUsableTime(couponTemplate, now) ||
                !IsCurrencyExpected(couponTemplate, order))
            {
                throw new CouponTemplateNotFoundOrUnavailableException();
            }

            var orderLinesInScope = GetOrderLinesInScope(couponTemplate, order, productDict);

            var models = await DiscountOrderLinesAsync(couponTemplate, coupon, order, orderLinesInScope);

            await _couponAppService.OccupyAsync(coupon.Id, new OccupyCouponInput { OrderId = order.Id });

            return models;
        }

        protected virtual bool IsCurrencyExpected(CouponTemplateData couponTemplate, Order order)
        {
            return couponTemplate.Currency == order.Currency;
        }

        protected virtual Task<List<OrderDiscountInfoModel>> DiscountOrderLinesAsync(CouponTemplateData couponTemplate,
            CouponData coupon, Order order, List<OrderLine> orderLinesInScope)
        {
            // Todo: support Custom coupon.
            if (couponTemplate.CouponType == CouponType.Custom)
            {
                throw new NotSupportedException();
            }

            var nodaCurrency = Currency.FromCode(order.Currency);
            var totalOrderLineActualTotalPrice =
                new Money(orderLinesInScope.Sum(x => x.ActualTotalPrice), nodaCurrency);

            var totalDiscountedAmount = new Money(
                couponTemplate.CouponType == CouponType.PerMeet
                    ? couponTemplate.DiscountAmount *
                      Math.Floor(totalOrderLineActualTotalPrice.Amount / couponTemplate.ConditionAmount)
                    : couponTemplate.DiscountAmount,
                nodaCurrency);

            if (totalDiscountedAmount > totalOrderLineActualTotalPrice)
            {
                totalDiscountedAmount = totalOrderLineActualTotalPrice;
            }

            var remainingDiscountedAmount = totalDiscountedAmount;

            var orderLineDiscounts = new Dictionary<OrderLine, Money>();

            foreach (var orderLine in orderLinesInScope)
            {
                var orderLineActualTotalPrice = new Money(orderLine.ActualTotalPrice, nodaCurrency);
                var maxDiscountAmount =
                    new Money(
                        orderLineActualTotalPrice.Amount / totalOrderLineActualTotalPrice.Amount *
                        totalDiscountedAmount.Amount, nodaCurrency, MidpointRounding.ToZero);

                var discountAmount = maxDiscountAmount > totalOrderLineActualTotalPrice
                    ? orderLineActualTotalPrice
                    : maxDiscountAmount;

                orderLineDiscounts[orderLine] = discountAmount;
                remainingDiscountedAmount -= discountAmount;
            }

            foreach (var orderLine in orderLinesInScope.OrderByDescending(x => x.ActualTotalPrice))
            {
                if (remainingDiscountedAmount == decimal.Zero)
                {
                    break;
                }

                var discountAmount = remainingDiscountedAmount > totalOrderLineActualTotalPrice
                    ? totalOrderLineActualTotalPrice
                    : remainingDiscountedAmount;

                orderLineDiscounts[orderLine] += discountAmount;
                remainingDiscountedAmount -= discountAmount;
            }

            if (remainingDiscountedAmount != decimal.Zero)
            {
                throw new ApplicationException();
            }

            order.SetProperty(CouponsConsts.OrderCouponDiscountAmountPropertyName, totalDiscountedAmount);

            var models = orderLinesInScope.Select(orderLine =>
                new OrderDiscountInfoModel(orderLine.Id, OrderDiscountName, coupon.Id.ToString(),
                    couponTemplate.DisplayName, orderLineDiscounts[orderLine].Amount)
            ).ToList();

            return Task.FromResult(models);
        }

        protected virtual List<OrderLine> GetOrderLinesInScope(CouponTemplateData couponTemplate, Order order,
            Dictionary<Guid, IProduct> productDict)
        {
            if (couponTemplate.IsUnscoped)
            {
                return order.OrderLines;
            }

            var expectedOrderLines = new List<OrderLine>();

            foreach (var scope in couponTemplate.Scopes.Where(scope => scope.StoreId == order.StoreId))
            {
                expectedOrderLines.AddRange(order.OrderLines
                    .WhereIf(scope.ProductGroupName != null,
                        x => productDict[x.ProductId].ProductGroupName == scope.ProductGroupName)
                    .WhereIf(scope.ProductId.HasValue, x => x.ProductId == scope.ProductId)
                    .WhereIf(scope.ProductSkuId.HasValue, x => x.ProductSkuId == scope.ProductSkuId));
            }

            if (expectedOrderLines.IsNullOrEmpty())
            {
                throw new OrderDoesNotMeetCouponUsageConditionException();
            }

            if (expectedOrderLines.Sum(x => GetOrderLineProductPrice(x, productDict) * x.Quantity) <
                couponTemplate.ConditionAmount)
            {
                throw new OrderDoesNotMeetCouponUsageConditionException();
            }

            return expectedOrderLines;
        }

        protected virtual decimal GetOrderLineProductPrice(OrderLine orderLine,
            Dictionary<Guid, IProduct> productDict)
        {
            return productDict[orderLine.ProductId].GetSkuById(orderLine.ProductSkuId).Price;
        }

        protected virtual bool IsInUsableTime(ICouponTemplate couponTemplate, DateTime now)
        {
            return couponTemplate.UsableBeginTime <= now && couponTemplate.UsableEndTime > now;
        }
    }
}