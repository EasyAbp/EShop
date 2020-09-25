using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public interface IHasCouponTemplateScopes<T> where T : ICouponTemplateScope
    {
        List<T> Scopes { get; }
    }
}