using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderCompletedEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }
        
        public OrderEto Order { get; set; }

        public OrderCompletedEto(OrderEto order)
        {
            TenantId = order.TenantId;
            Order = order;
        }
    }
}