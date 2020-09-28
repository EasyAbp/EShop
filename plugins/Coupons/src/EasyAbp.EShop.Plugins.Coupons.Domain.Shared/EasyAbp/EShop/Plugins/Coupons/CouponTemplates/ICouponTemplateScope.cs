using System;
using EasyAbp.EShop.Stores.Stores;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public interface ICouponTemplateScope : IMultiStore
    {
        [CanBeNull]
        string ProductGroupName { get; }

        Guid? ProductId { get; }
        
        Guid? ProductSkuId { get; }
    }
}