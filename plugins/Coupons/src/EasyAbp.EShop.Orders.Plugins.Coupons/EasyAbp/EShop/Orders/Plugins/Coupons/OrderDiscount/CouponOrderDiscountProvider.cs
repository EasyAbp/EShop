using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponOrderDiscountProvider : IOrderDiscountProvider, ITransientDependency
    {
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
        
        public virtual async Task<Order> DiscountAsync(Order order, Dictionary<Guid, ProductDto> productDict)
        {
            var couponId = order.GetProperty<Guid?>(CouponsConsts.OrderCouponIdPropertyName);
            if (couponId is null)
            {
                return order;
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
            
            DiscountOrderLines(couponTemplate, order, orderLinesInScope);
            
            await _couponAppService.OccupyAsync(coupon.Id, new OccupyCouponInput {OrderId = order.Id});

            return order;
        }

        protected virtual bool IsCurrencyExpected(CouponTemplateData couponTemplate, Order order)
        {
            return couponTemplate.Currency == order.Currency;
        }

        protected virtual void DiscountOrderLines(CouponTemplateData couponTemplate, Order order,
            List<OrderLine> orderLinesInScope)
        {
            // Todo: support Custom coupon.
            if (couponTemplate.CouponType == CouponType.Custom)
            {
                throw new NotSupportedException();
            }
            
            var totalOrderLineActualTotalPrice = orderLinesInScope.Sum(x => x.ActualTotalPrice);

            var totalDiscountedAmount = couponTemplate.CouponType == CouponType.PerMeet
                ? couponTemplate.DiscountAmount *
                  Math.Floor(totalOrderLineActualTotalPrice / couponTemplate.ConditionAmount)
                : couponTemplate.DiscountAmount;

            if (totalDiscountedAmount > totalOrderLineActualTotalPrice)
            {
                totalDiscountedAmount = totalOrderLineActualTotalPrice;
            }

            var remainingDiscountedAmount = totalDiscountedAmount;

            // Todo: https://github.com/EasyAbp/EShop/issues/104
            const int accuracy = 2;
            
            foreach (var orderLine in orderLinesInScope)
            {
                var maxDiscountAmount =
                    Math.Round(orderLine.ActualTotalPrice / totalOrderLineActualTotalPrice * totalDiscountedAmount,
                        accuracy, MidpointRounding.ToZero);

                var discountAmount = maxDiscountAmount > orderLine.ActualTotalPrice
                    ? orderLine.ActualTotalPrice
                    : maxDiscountAmount;

                order.AddDiscount(orderLine.Id, discountAmount);

                remainingDiscountedAmount -= discountAmount;
            }

            foreach (var orderLine in orderLinesInScope.OrderByDescending(x => x.ActualTotalPrice))
            {
                if (remainingDiscountedAmount == decimal.Zero)
                {
                    break;
                }
                
                var discountAmount = remainingDiscountedAmount > orderLine.ActualTotalPrice
                    ? orderLine.ActualTotalPrice
                    : remainingDiscountedAmount;
                
                order.AddDiscount(orderLine.Id, discountAmount);

                remainingDiscountedAmount -= discountAmount;
            }

            if (remainingDiscountedAmount != decimal.Zero)
            {
                throw new ApplicationException();
            }
            
            order.SetProperty(CouponsConsts.OrderCouponDiscountAmountPropertyName, totalDiscountedAmount);
        }

        protected virtual List<OrderLine> GetOrderLinesInScope(CouponTemplateData couponTemplate, Order order,
            Dictionary<Guid, ProductDto> productDict)
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

        protected virtual decimal GetOrderLineProductPrice(OrderLine orderLine, Dictionary<Guid, ProductDto> productDict)
        {
            return productDict[orderLine.ProductId].GetSkuById(orderLine.ProductSkuId).Price;
        }

        protected virtual bool IsInUsableTime(ICouponTemplate couponTemplate, DateTime now)
        {
            return couponTemplate.UsableBeginTime <= now && couponTemplate.UsableEndTime > now;
        }
    }
}