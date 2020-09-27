using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponOrderCreatedEventHandler : IDistributedEventHandler<EntityCreatedEto<OrderEto>>, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly ICouponRepository _couponRepository;

        public CouponOrderCreatedEventHandler(
            IClock clock,
            ICouponRepository couponRepository)
        {
            _clock = clock;
            _couponRepository = couponRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityCreatedEto<OrderEto> eventData)
        {
            if (!Guid.TryParse(eventData.Entity.GetProperty<string>(CouponsConsts.OrderCouponIdPropertyName),
                out var couponId))
            {
                return;
            }
            
            var discountAmount = eventData.Entity.GetProperty<decimal>(CouponsConsts.OrderCouponDiscountAmountPropertyName);

            var coupon = await _couponRepository.GetAsync(couponId);

            if (coupon.OrderId != eventData.Entity.Id)
            {
                throw new InvalidCouponOrderIdException(eventData.Entity.Id, coupon.OrderId);
            }
            
            coupon.SetUsed(_clock.Now, discountAmount);

            await _couponRepository.UpdateAsync(coupon, true);
        }
    }
}