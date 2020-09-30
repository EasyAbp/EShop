using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponOrderCanceledEventHandler : IDistributedEventHandler<OrderCanceledEto>, ITransientDependency
    {
        private readonly ICouponRepository _couponRepository;

        public CouponOrderCanceledEventHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(OrderCanceledEto eventData)
        {
            if (!Guid.TryParse(eventData.Order.GetProperty<string>(CouponsConsts.OrderCouponIdPropertyName),
                out var couponId))
            {
                return;
            }

            var coupon = await _couponRepository.GetAsync(couponId);
            
            coupon.SetOrderId(null);
            coupon.SetUsed(null, null, null);

            await _couponRepository.UpdateAsync(coupon, true);
        }
    }
}