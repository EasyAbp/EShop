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
        public static string OrderDiscountEffectGroup { get; set; } = "Coupon";

        public static int CouponOrderDiscountEffectOrder { get; set; } = 5000;

        public int EffectOrder => CouponOrderDiscountEffectOrder;

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

        public virtual async Task DiscountAsync(OrderDiscountContext context)
        {
            var couponId = context.Order.GetProperty<Guid?>(CouponsConsts.OrderCouponIdPropertyName);

            if (couponId is null)
            {
                return;
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
                !IsCurrencyExpected(couponTemplate, context.Order))
            {
                throw new CouponTemplateNotFoundOrUnavailableException();
            }

            var orderLinesInScope = GetOrderLinesInScope(couponTemplate, context);

            var model = await CreateDiscountModelAsync(context, couponTemplate, coupon, orderLinesInScope);

            context.CandidateDiscounts.Add(model);

            await _couponAppService.OccupyAsync(coupon.Id, new OccupyCouponInput { OrderId = context.Order.Id });
        }

        protected virtual bool IsCurrencyExpected(CouponTemplateData couponTemplate, IOrder order)
        {
            return couponTemplate.Currency == order.Currency;
        }

        protected virtual Task<OrderDiscountInfoModel> CreateDiscountModelAsync(OrderDiscountContext context,
            CouponTemplateData couponTemplate, CouponData coupon, List<IOrderLine> orderLinesInScope)
        {
            // Todo: support Custom coupon.
            if (couponTemplate.CouponType == CouponType.Custom)
            {
                throw new NotSupportedException();
            }

            var nodaCurrency = Currency.FromCode(context.Order.Currency);
            var totalOrderLineActualTotalPrice =
                new Money(orderLinesInScope.Sum(x => x.ActualTotalPrice), nodaCurrency);

            var totalDiscountedAmount = new Money(
                couponTemplate.CouponType == CouponType.PerMeet
                    ? couponTemplate.DiscountAmount *
                      Math.Floor(totalOrderLineActualTotalPrice.Amount / couponTemplate.ConditionAmount)
                    : couponTemplate.DiscountAmount,
                nodaCurrency, MidpointRounding.ToZero);

            if (totalDiscountedAmount > totalOrderLineActualTotalPrice)
            {
                totalDiscountedAmount = totalOrderLineActualTotalPrice;
            }

            context.Order.SetProperty(CouponsConsts.OrderCouponDiscountAmountPropertyName, totalDiscountedAmount);

            var model = new OrderDiscountInfoModel(orderLinesInScope.Select(x => x.Id).ToList(),
                OrderDiscountEffectGroup, OrderDiscountName, coupon.Id.ToString(), couponTemplate.DisplayName,
                new DynamicDiscountAmountModel(context.Order.Currency, totalDiscountedAmount.Amount, 0m, null));
            // todo: discount rate for coupons.

            return Task.FromResult(model);
        }

        protected virtual List<IOrderLine> GetOrderLinesInScope(CouponTemplateData couponTemplate,
            OrderDiscountContext context)
        {
            List<IOrderLine> expectedOrderLines;

            if (couponTemplate.IsUnscoped)
            {
                expectedOrderLines = context.Order.OrderLines.ToList();
            }
            else
            {
                expectedOrderLines = [];
                foreach (var scope in couponTemplate.Scopes.Where(scope => scope.StoreId == context.Order.StoreId))
                {
                    expectedOrderLines.AddRange(context.Order.OrderLines
                        .WhereIf(scope.ProductGroupName != null,
                            x => context.ProductDict[x.ProductId].ProductGroupName == scope.ProductGroupName)
                        .WhereIf(scope.ProductId.HasValue, x => x.ProductId == scope.ProductId)
                        .WhereIf(scope.ProductSkuId.HasValue, x => x.ProductSkuId == scope.ProductSkuId));
                }
            }

            if (expectedOrderLines.IsNullOrEmpty())
            {
                throw new OrderDoesNotMeetCouponUsageConditionException();
            }

            if (expectedOrderLines.Sum(x => GetOrderLineProductPrice(x, context) * x.Quantity) <
                couponTemplate.ConditionAmount)
            {
                throw new OrderDoesNotMeetCouponUsageConditionException();
            }

            return expectedOrderLines;
        }

        protected virtual decimal GetOrderLineProductPrice(IOrderLine orderLine, OrderDiscountContext context)
        {
            return context.ProductDict[orderLine.ProductId].GetSkuById(orderLine.ProductSkuId).Price;
        }

        protected virtual bool IsInUsableTime(ICouponTemplate couponTemplate, DateTime now)
        {
            return couponTemplate.UsableBeginTime <= now && couponTemplate.UsableEndTime > now;
        }
    }
}