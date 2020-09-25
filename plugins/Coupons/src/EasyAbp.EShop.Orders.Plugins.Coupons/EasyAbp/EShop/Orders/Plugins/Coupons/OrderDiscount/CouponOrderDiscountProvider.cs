using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponOrderDiscountProvider : IOrderDiscountProvider, ITransientDependency
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICouponLookupService _couponLookupService;
        private readonly ICouponTemplateLookupService _couponTemplateLookupService;

        public CouponOrderDiscountProvider(
            IOrderRepository orderRepository,
            ICouponLookupService couponLookupService,
            ICouponTemplateLookupService couponTemplateLookupService)
        {
            _orderRepository = orderRepository;
            _couponLookupService = couponLookupService;
            _couponTemplateLookupService = couponTemplateLookupService;
        }
        
        public virtual async Task<Order> DiscountAsync(Order order)
        {
            if (!Guid.TryParse(order.GetProperty<string>(CouponsConsts.OrderCouponIdPropertyName),
                out var couponId))
            {
                return order;
            }

            var coupon = await _couponLookupService.FindByIdAsync(couponId);

            var couponTemplate = await _couponTemplateLookupService.FindByIdAsync(coupon.CouponTemplateId);

            // Todo: consume the coupon!!!
            
            // Todo: get discounted order lines
            var discountedAmount = order.AddDiscountGetRealDiscountedAmount(couponTemplate.DiscountAmount);

            order.SetProperty(CouponsConsts.OrderCouponDiscountAmountPropertyName, discountedAmount);
            
            await _orderRepository.UpdateAsync(order, true);

            return order;
        }
    }
}