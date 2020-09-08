using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class CompleteOrderEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid OrderId { get; set; }
    }
}