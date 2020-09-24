using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Data;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.Authorization
{
    public class CouponOrderCreationAuthorizationHandler : OrderCreationAuthorizationHandler
    {
        private readonly IClock _clock;
        private readonly ICouponLookupService _couponLookupService;
        private readonly ICouponTemplateLookupService _couponTemplateLookupService;

        public CouponOrderCreationAuthorizationHandler(
            IClock clock,
            ICouponLookupService couponLookupService,
            ICouponTemplateLookupService couponTemplateLookupService)
        {
            _clock = clock;
            _couponLookupService = couponLookupService;
            _couponTemplateLookupService = couponTemplateLookupService;
        }
        
        protected override async Task HandleOrderCreationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, OrderCreationResource resource)
        {
            var couponId = resource.Input.GetProperty<Guid?>(CouponsConsts.OrderCouponIdPropertyName);

            if (!couponId.HasValue)
            {
                return;
            }

            var now = _clock.Now;
            
            var coupon = await _couponLookupService.FindByIdAsync(couponId.Value);

            if (coupon == null || coupon.ExpirationTime < now)
            {
                context.Fail();
                return;
            }
            
            var couponTemplate = await _couponTemplateLookupService.FindByIdAsync(coupon.CouponTemplateId);

            if (couponTemplate == null ||
                !IsInUsableTime(couponTemplate, now) ||
                !IsOrderInScope(couponTemplate, resource))
            {
                context.Fail();
                return;
            }
        }

        protected virtual bool IsOrderInScope(ICouponTemplate couponTemplate, OrderCreationResource resource)
        {
            if (couponTemplate.IsUnscoped)
            {
                return true;
            }

            var expectedOrderLines = new List<CreateOrderLineDto>();

            foreach (var scope in couponTemplate.Scopes)
            {
                expectedOrderLines.AddRange(resource.Input.OrderLines
                    .WhereIf(scope.ProductGroupName != null,
                        x => resource.ProductDictionary[x.ProductId].ProductGroupName == scope.ProductGroupName)
                    .WhereIf(scope.ProductId.HasValue, x => x.ProductId == scope.ProductId)
                    .WhereIf(scope.ProductSkuId.HasValue, x => x.ProductSkuId == scope.ProductSkuId));
            }

            if (couponTemplate.IsCrossProductAllowed)
            {
                return expectedOrderLines.Sum(x => GetOrderLineProductPrice(x, resource) * x.Quantity) >=
                       couponTemplate.ConditionAmount;
            }

            return expectedOrderLines.Any(orderLine =>
                GetOrderLineProductPrice(orderLine, resource) >= couponTemplate.ConditionAmount);
        }

        protected virtual decimal GetOrderLineProductPrice(CreateOrderLineDto createOrderLineDto, OrderCreationResource resource)
        {
            return resource.ProductDictionary[createOrderLineDto.ProductId].GetSkuById(createOrderLineDto.ProductSkuId).Price;
        }

        protected virtual bool IsInUsableTime(ICouponTemplate couponTemplate, DateTime now)
        {
            return couponTemplate.UsableBeginTime <= now && couponTemplate.UsableEndTime > now;
        }
    }
}