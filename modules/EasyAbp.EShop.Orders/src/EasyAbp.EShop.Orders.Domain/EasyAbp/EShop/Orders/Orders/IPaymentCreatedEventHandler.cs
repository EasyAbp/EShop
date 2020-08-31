﻿using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IPaymentCreatedEventHandler : IDistributedEventHandler<EntityCreatedEto<EShopPaymentEto>>
    {
        
    }
}